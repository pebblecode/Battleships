module Battleships 
    open System

    type Orientation = Horizontal | Vertical
    type HitResult = 
        | Miss 
        | Hit of string 
        | Sunk of string
        static member (+) (hr1:HitResult, hr2:HitResult) = 
            match (hr1, hr2) with
            | Miss, Miss -> Miss
            | _ as output, Miss -> output
            | Miss, (_ as output)-> output

    type Point(x:int, y:int) =
        member val X = x with get
        member val Y = y with get

        member this.Equals (p:Point) = this.X.Equals(p.X) && this.Y.Equals(p.Y)

        member this.Translate orientation offset = 
            match orientation with
            | Horizontal -> new Point(this.X + offset, this.Y)
            | Vertical -> new Point(this.X, this.Y + offset)
        override this.ToString() = sprintf "%d,%d" this.X this.Y

    // Simple ship implementation - a ship has a collection of points that it occupies
    // When hit, the points are removed.  Once all points are removed, fin
    type Ship (name:string, points:Point list) = 
        member val Points = points with get
        member val Name = name with get
        member this.IsSunk = this.Points.Length = 0
        member private this.HasPoint (point:Point) = List.exists point.Equals this.Points
        override this.ToString() = sprintf "%s: %A" this.Name this.Points
        
        member this.TryHit (target:Point) = 
            let shipPoints' = List.filter (fun x -> not (x.Equals target)) this.Points
            let hitResult = 
                match this.HasPoint target with
                | true when shipPoints'.Length = 0 -> Sunk this.Name
                | true -> Hit this.Name
                | _ -> Miss
            (hitResult, new Ship(this.Name, shipPoints'))
        
        member this.CollidesWith (others:Ship list) = others |> List.exists (fun other -> List.exists other.HasPoint this.Points)

    type ShipBuilder = unit -> Ship

    type ShipFactory(gridSize:int) =

        let BuildShip (length:int) (name:string) = 
            let rnd = new System.Random()
            let startPoint = 
                let rndPoint() = rnd.Next(gridSize - length)
                new Point(rndPoint(), rndPoint())
            let orientation = match rnd.Next(2) with 
                                | 0 -> Horizontal 
                                | 1 -> Vertical
            let points = [0 .. length-1] |> List.map (fun i -> startPoint.Translate orientation i)
            new Ship(name, points)

        member this.BuildDestroyer() = BuildShip 4 "Destroyer"
        member this.BuildBattleship() = BuildShip 5 "Battleship"
                
    type Board(ships:Ship list) = 
        member val Ships = ships with get

        static member BuildRandom (boardSize:int) =
            let factory = new ShipFactory(boardSize)

            //Will repeatedly ask for a randomly placed ship until it fits with ships already placed
            let rec findSpaceForShip existingShips (shipBuilderFn:ShipBuilder) = 
                let ship = shipBuilderFn()
                match (ship.CollidesWith existingShips) with
                | true -> findSpaceForShip existingShips shipBuilderFn 
                | false -> ship :: existingShips

            //Glue it together - for each builder fn, place randomly til it fits, lastly create the Board 
            [ factory.BuildBattleship ; factory.BuildDestroyer ; factory.BuildDestroyer ]
            |> List.fold findSpaceForShip []
            |> fun initialState -> new Board(initialState)

        member this.ShowBoard() = List.iter (fun ship -> printfn "%A" ship) this.Ships

        //Returns a tuple - first arg indicates success of hit, second is new board state
        member this.TryHit (p:Point) = 
            let shipWasHit (results:list<HitResult * Ship>) = List.map fst results |> List.reduce (+)
            let hitResults = List.map (fun (ship:Ship) -> ship.TryHit(p)) this.Ships
            (shipWasHit hitResults), Board(List.map snd hitResults)

        member this.ShipsRemain = not (List.forall (fun (s:Ship) -> s.IsSunk) this.Ships )

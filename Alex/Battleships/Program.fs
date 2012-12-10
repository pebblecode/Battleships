open Battleships
open System

[<EntryPoint>]
let main argv = 
    let getInput() = 
        printfn "Input target: x,y\n"
        let coords = Console.ReadLine()
        let grabInt (str:string) idx = str.Split(',').[idx] |> Int32.Parse
        try
            Some (new Point(grabInt coords 0, grabInt coords 1))
        with
            | _ -> None

    let rec fireAtBoard (board:Board) = 
        board.ShowBoard()
        let target = getInput()
        match target with
        | Some (targetPoint) -> 
            let result = board.TryHit(targetPoint)
            printfn "%A" (fst result)
            match board.ShipsRemain with
            | false -> printfn "GAME OVER"
            | true -> 
                let board' = snd result
                fireAtBoard board'
        | None -> fireAtBoard board

    fireAtBoard (Board.BuildRandom 10)    
    0

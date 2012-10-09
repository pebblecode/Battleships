using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Datalayer
{
   public class RandomShipFactory
    {
       
       private Random rand = new Random();
       public Ships.Ship GetBattleship()
       {     
           var ship =  new Ships.Battleship(getRadomCell(), getRadomOrientation());

           //Only return a ship within the 10 by 10 board
           while (ship.Cells.Any(z => (int)z.Column > 10 || z.Row > 10))
           {
               ship = new Ships.Battleship(getRadomCell(), getRadomOrientation());
           }

           return ship;
       }
       private Datalayer.Enums.Orientation getRadomOrientation()
       {           
           var orientation = (Datalayer.Enums.Orientation)rand.Next(1, 3);
           return orientation;
       }

       private BoardCell getRadomCell()
       {
           Random rand = new Random();
           BoardCell cell = new BoardCell();
           cell.Row = rand.Next(1, 11);
           cell.Column = (Enums.BoardColumn)rand.Next(1, 11);
           return cell;
       }

       public Ships.Ship GetDestroyer()
       {
           var ship = new Ships.Destroyer(getRadomCell(), getRadomOrientation());

           //Only return a ship within the 10 by 10 board
           while (ship.Cells.Any(z => (int)z.Column > 10 || z.Row > 10))
           {
               ship = new Ships.Destroyer(getRadomCell(), getRadomOrientation());
           }

           return ship;
       }
    }
}

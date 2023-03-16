using BingoWebApp.Reglas;

namespace BingoWebApp.Models
{
    public class cartonBingo
    {
        public int NumeroDeCarton { get; set; }

        public bool CartonLleno { get; set; }
        public int Coincide1 { get; set; }
        public int Coincide2 { get; set; }
        public int Coincide3 { get; set; }
        public int Coincide4 { get; set; }


        public int [,] CartonNumeros { get; set; }

        public List<int> BolillasAModelo { get; set; }
    }
}

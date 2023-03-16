using BingoWebApp.Models;
using System.Runtime.Intrinsics.X86;

namespace BingoWebApp.Reglas
{
    public class ReglasBingo
    {
        private int posicion = 0;
        public int[,] GenerarCarton()
        {
            cartonBingo carton = new()
            {
                CartonNumeros = new int[3, 9]
            };

            var n = new Random();

            for (int c = 0; c < 9; c++)
            {
                for (int f = 0; f < 3; f++)
                {
                    bool numeroUnico = false;
                    int nuevoNumero = 0;
                    while (!numeroUnico)
                    {
                        // Se ordenan las columnas por decenas
                        if (c == 0)
                        {
                            nuevoNumero = n.Next(1, 10);
                        }
                        else if (c == 8)
                        {
                            nuevoNumero = n.Next(80, 91);
                        }
                        else
                        {
                            nuevoNumero = n.Next(c * 10, c * 10 + 10);
                        }

                        // Verifico que no haya números repetidos
                        numeroUnico = true;
                        for (int g = 0; g < 3; g++)
                        {
                            if (carton.CartonNumeros[g, c] == nuevoNumero)
                            {
                                numeroUnico = false; break;
                            }
                        }
                    }

                    carton.CartonNumeros[f, c] = nuevoNumero;
                }
            }

            // Ordenando el carton
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int k = j + 1; k < 3; k++)
                    {
                        if (carton.CartonNumeros[j, i] > carton.CartonNumeros[k, i])
                        {
                            int aux = carton.CartonNumeros[j, i];
                            carton.CartonNumeros[j, i] = carton.CartonNumeros[k, i];
                            carton.CartonNumeros[k, i] = aux;
                        }
                    }
                }
            }

            var ceros = 0;
            var filaABorrar = 0;
            var columnaABorrar = 0;
            do
            {
                filaABorrar = n.Next(0, 3);
                columnaABorrar = n.Next(0, 9);
                if (carton.CartonNumeros[filaABorrar, columnaABorrar] == 0)
                {
                    continue;
                }

                // Cuento ceros por fila
                var cerosEnFila = 0;
                for (int c = 0; c < 9; c++)
                {
                    if (carton.CartonNumeros[filaABorrar, c] == 0)
                    {
                        cerosEnFila++;
                    }
                }

                // Cuento ceros por columna
                var cerosEnColumna = 0;
                for (int f = 0; f < 3; f++)
                {
                    if (carton.CartonNumeros[f, columnaABorrar] == 0)
                    {
                        cerosEnColumna++;
                    }
                }

                // Cuento números por columna
                int unItemPorColumna = 0;
                int[] itemsPorColumna = new int[9];
                for (int c = 0; c < 9; c++)
                {
                    for (int f = 0; f < 3; f++)
                    {
                        if (carton.CartonNumeros[f, c] != 0)
                        {
                            itemsPorColumna[c]++;
                        }
                    }
                }

                // Cuento columnas con un solo número
                for (int i = 0; i < 9; i++)
                {
                    if (itemsPorColumna[i] == 1)
                    {
                        unItemPorColumna++;
                    }
                }

                // El carton debe tener 4 ceros por fila o no mas de dos por columna
                if (cerosEnFila == 4 || cerosEnColumna == 2)
                {
                    continue;
                }

                // El carton debe tener 3 columnas con un solo número y ninguna columna con 3 números
                if (unItemPorColumna == 3 && itemsPorColumna[columnaABorrar] != 3)
                {
                    continue;
                }

                carton.CartonNumeros[filaABorrar, columnaABorrar] = 0;
                ceros++;

            } while (ceros < 12);

            return carton.CartonNumeros;
        }

        public List<int> LlenarBolillero()
        {
            List<int> lasBolillas = new List<int>();
            //Bolillero nuevasBolillas = new()
            //{
            //    Bolillas = new List<int>()
            //};
            var a = new Random();
            bool esDistinto;

            for (int i = 0; i <= 89; i++) //Este bucle for itera para llenar el vector que contiene las "bolillas"
            {
                int aux; // variable auxiliar para almacenar el número random

                do
                {
                    aux = a.Next(1, 91); // Nos arroja un número entre 1 y 90.                   
                    esDistinto = true;
                    for (int j = 0; j < lasBolillas.Count; j++) // Este bucle for va a comparar el valor aux con cada valor del vector.
                    {
                        if (lasBolillas[j] == aux) // Si el numero se repite, corta el for que verifica y vuelve a sortear otro numero.
                        {
                            esDistinto = false; break;
                        }
                    }

                } while (esDistinto == false); // No saldra de bucle hasta que se haya encontrado un número distinto a los que esten en el vector.
                //lasBolillas[i] = aux;
                lasBolillas.Add(aux);
            }
            return lasBolillas;
        }

        //public List<int> LlenarBolillero() otro metodo que tambien funciona, lo dejo para no olvidar
        //{
        //    List<int> bolillero = new List<int>();
        //    for (int i = 0; i < 90; i++)
        //    {
        //        bolillero.Add(i + 1);
        //    }
        //    var ran = new Random();
        //    var p = 0;
        //    for (int i = 0; i < bolillero.Count; i++)
        //    {
        //        bolillero[i] = i + 1;
        //    }
        //    for (int i = p; i < bolillero.Count; i++)
        //    {
        //        int j = ran.Next(p + 1, bolillero.Count);
        //        var temp = bolillero[i];
        //        bolillero[i] = bolillero[j];
        //        bolillero[j] = temp;
        //    }
        //    return bolillero;
        //}

        public int SortearBolilla(List<int> Bolillas) // Esta funcion obtiene el valor de una posicion del vector, el numero de posicion aumenta con cada llamada de la funcion.
        {
            var sorteada = Bolillas[posicion];
            posicion++;
            return (int)sorteada;
        }
    }
}


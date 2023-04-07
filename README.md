# BingoMVC
Proyecto final del curso de .NET 6 dictado por el Polo Tecnológico de Mina Clavero

Este proyecto, es un sitio web realizado en arquitectura MVC con el framework .Net 6
Consiste en un juego de Bingo tradicional, donde para jugar, primero el sistema genera automáticamente 4 cartones con las siguientes características:

- Matriz bi-dimensional de 3 x 9
- Tienen 15 números aleatorios
- Números no repetidos 
- Ordenados por decenas del 1 al 90
- Poseen 12 espacios vacíos, 4 por cada fila
- Solo 3 columnas pueden tener 2 espacios vacíos
- Ninguna columna puede o no tener espacios vacíos o tener 3 espacios vacíos

Luego tenemos la funcionalidad del Bolillero, el cual tiene la característica de ser un vector de 90 números enteros, desordenados y no repetidos.
Este bolillero posee un botón que nos permite mostrar la bollilla sacada. En caso de coincidencia de valor dentro de alguno de los cartones generados,
esta se pinta de color rojo. En cuanto un primer carton llegue a conseguir las 15 coincidencias, es declarado ganador. 

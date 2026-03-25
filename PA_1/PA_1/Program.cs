using System;
namespace pa1
{
    class program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            string operador, nTurno;
            int capacidadT, ticketsCreados = 0, ticketsCerrados = 0, tiempo = 0;
            double recaudado = 0;

            string placa = "", cliente = "";
            int vehiculo = 0, minutoEntrada = 0;
            bool ticketActivo = false, clientevip = false;

            int opcion = 0, minutosSimulados = 0;
            double totalAPagar = 0;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("--- SISTEMA DE PARQUEO ---");
            Console.ResetColor();

            Console.WriteLine(@"  ______
 /|_||_\`.__
(   _    _ _\
=`-(_)--(_)-'
");
            Console.Write("Ingrese nombre del operador: ");
            operador = Console.ReadLine();

            do
            {
                Console.Write("Ingrese código de turno (4 caracteres): ");
                nTurno = Console.ReadLine();
                if (nTurno.Length != 4)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR: EL CODIGO DE TURNO DEBE TENER 4 CARACTERES");
                    Console.ResetColor();
                }
            } while (nTurno.Length != 4);

            do
            {
                Console.Write("Ingrese capacidad del parqueo (Mínimo 10): ");
                if (!int.TryParse(Console.ReadLine(), out capacidadT) || capacidadT < 10)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR: INGRESE UN NUMERO ENTERO MAYOR O IGUAL A 10");
                    Console.ResetColor();
                    capacidadT = 0;
                }
            } while (capacidadT < 10);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Sistema inicializado correctamente");
            Console.ResetColor();

            do
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("--- MENÚ ---");
                Console.WriteLine("1. Crear Ticket");
                Console.WriteLine("2. Registrar Salida y Cobro");
                Console.WriteLine("3. Consultar Estado del Parqueo");
                Console.WriteLine("4. Simular Paso del Tiempo");
                Console.WriteLine("5. Salir del Turno");
                Console.Write("Seleccione una opción: ");
                Console.ResetColor();

                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    opcion = 0;
                }

                switch (opcion)
                {
                    case 1:
                        Console.WriteLine("Has elegido: Crear Ticket");
                        if (ticketActivo)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("ERROR: YA EXISTE UN TICKET ACTIVO");
                            Console.ResetColor();
                        }
                        else if (ticketsCerrados >= capacidadT)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("ERROR: EL PARQUEO HA ALCANZADO SU CAPACIDAD MAXIMA PARA ESTE TURNO");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\n--- REGISTRO DE ENTRADA ---");
                            Console.ResetColor();

                            do
                            {
                                Console.Write("Ingrese la placa (6-8 caracteres, sin espacios): ");
                                placa = Console.ReadLine().Replace(" ", "");
                                if (placa.Length < 6 || placa.Length > 8)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("ERROR: LA PLACA DEBE TENER ENTRE 6 Y 8 CARACTERES");
                                    Console.ResetColor();
                                }
                            } while (placa.Length < 6 || placa.Length > 8);

                            do
                            {
                                Console.WriteLine("Tipo de Vehículo: 1. Moto 2. Auto 3. SUV");
                                Console.Write("Seleccione una opción: ");
                                if (!int.TryParse(Console.ReadLine(), out vehiculo) || vehiculo < 1 || vehiculo > 3)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("ERROR: SELECCIONE UN TIPO VALIDO (1, 2, 3)");
                                    Console.ResetColor();
                                    vehiculo = 0;
                                }
                            } while (vehiculo == 0);

                            Console.Write("Nombre del cliente: ");
                            cliente = Console.ReadLine();

                            Console.Write("¿Es cliente VIP? (S/N): ");
                            clientevip = Console.ReadLine().ToUpper() == "S";

                            minutoEntrada = tiempo;
                            ticketActivo = true;
                            ticketsCreados++;

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Ticket creado exitosamente en el minuto " + minutoEntrada);
                            Console.ResetColor();
                        }
                        break;

                    case 2:
                        Console.WriteLine("Has elegido: Registrar Salida");
                        if (!ticketActivo)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("ERROR: NO HAY NINGUN VEHICULO EN EL PARQUEO ACTUALMENTE");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("\n--- PROCESO DE SALIDA Y COBRO ---");
                            Console.ResetColor();

                            int tiempoEstancia = tiempo - minutoEntrada;

                            double tarifa = 0;
                            string tipoTexto = "";
                            if (vehiculo == 1) { tarifa = 5; tipoTexto = "Moto"; }
                            else if (vehiculo == 2) { tarifa = 10; tipoTexto = "Auto"; }
                            else { tarifa = 15; tipoTexto = "SUV"; }

                            double horasACobrar = Math.Ceiling(tiempoEstancia / 60.0);
                            double cobroBase = horasACobrar * tarifa;

                            double totalFinal = cobroBase;

                            if (tiempoEstancia <= 15)
                            {
                                totalFinal = 0;
                                Console.WriteLine("Tiempo menor a 15 min: Aplicó cortesía (Q0.00)");
                            }
                            else
                            {
                                if (tiempoEstancia > 360)
                                {
                                    totalFinal += 25;
                                    Console.WriteLine("Estancia mayor a 6 horas: Se aplicó multa de Q25.00");
                                }

                                if (clientevip)
                                {
                                    totalFinal = totalFinal * 0.90;
                                    Console.WriteLine("Cliente VIP: Se aplicó 10% de descuento.");
                                }
                            }

                            Console.WriteLine("----------------------------------------");
                            Console.WriteLine("Vehículo: " + tipoTexto + " | Placa: " + placa);
                            Console.WriteLine("Tiempo total: " + tiempoEstancia + " minutos (" + horasACobrar + " hrs cobradas)");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("TOTAL A PAGAR: Q" + totalFinal.ToString("F2"));
                            Console.ResetColor();

                            recaudado += totalFinal;
                            ticketsCerrados++;
                            ticketActivo = false;
                            Console.WriteLine("----------------------------------------");
                            Console.WriteLine("Salida registrada. Espacio liberado.");
                        }
                        break;

                    case 3:
                        Console.WriteLine("Has elegido: Ver Estado");
                        int ocupados = ticketActivo ? 1 : 0;
                        int disponibles = capacidadT - ocupados;

                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("\n========================================");
                        Console.WriteLine("       ESTADO GENERAL DEL PARQUEO       ");
                        Console.WriteLine("========================================");
                        Console.ResetColor();

                        Console.WriteLine("Capacidad Total: " + capacidadT);
                        Console.WriteLine("Espacios Ocupados: " + ocupados);
                        Console.WriteLine("Espacios Disponibles: " + disponibles);

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("----------------------------------------");
                        Console.WriteLine("Tiempo Simulado Actual: " + tiempo + " minutos");
                        Console.WriteLine("Total Recaudado: Q" + recaudado.ToString("F2"));
                        Console.WriteLine("----------------------------------------");
                        Console.ResetColor();

                        Console.WriteLine("Tickets Creados en el turno: " + ticketsCreados);
                        Console.WriteLine("Tickets Cerrados (Salidas): " + ticketsCerrados);
                        Console.WriteLine("========================================\n");
                        break;

                    case 4:
                        Console.WriteLine("Has elegido: Simular Tiempo");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("\n--- SIMULACIÓN DE TIEMPO ---");
                        Console.ResetColor();

                        int minutosASimular = 0;
                        do
                        {
                            Console.Write("¿Cuántos minutos desea adelantar? (1 - 1440): ");
                            if (!int.TryParse(Console.ReadLine(), out minutosASimular) || minutosASimular < 1 || minutosASimular > 1440)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("ERROR: INGRESE UN TIEMPO VALIDO ENTRE 1 Y 1440 MINUTOS (24 horas)");
                                Console.ResetColor();
                                minutosASimular = 0;
                            }
                        } while (minutosASimular == 0);

                        tiempo += minutosASimular;

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("El tiempo ha avanzado. Tiempo total simulado: " + tiempo + " minutos");
                        Console.ResetColor();
                        break;

                    case 5:
                        Console.WriteLine("Cerrando turno...");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("\n========================================");
                        Console.WriteLine("       RESUMEN FINAL DEL TURNO          ");
                        Console.WriteLine("========================================");
                        Console.ResetColor();

                        Console.WriteLine("Operador: " + operador);
                        Console.WriteLine("Código de Turno: " + nTurno);
                        Console.WriteLine("----------------------------------------");
                        Console.WriteLine("Total de Tickets Creados: " + ticketsCreados);
                        Console.WriteLine("Total de Tickets Cerrados: " + ticketsCerrados);

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("TOTAL RECAUDADO EN EL TURNO: Q" + recaudado.ToString("F2"));
                        Console.ResetColor();

                        Console.WriteLine("========================================");
                        Console.WriteLine("Cerrando sistema... Presione cualquier tecla.");
                        Console.ReadKey();
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("ERROR: OPCION NO VALIDA. INTENTE DE NUEVO.");
                        Console.ResetColor();
                        break;
                }

            } while (opcion != 5);
        }
    }
}
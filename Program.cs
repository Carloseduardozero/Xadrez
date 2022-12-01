using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;
using xadrez;

namespace Xadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            

            try
            {
              PartidaDeXadrez partida = new PartidaDeXadrez();

                while (!partida.terminada) 
                {
                    Console.Clear();
                    Tela.imprimirTabuleiro(partida.tab);

                    Console.ReadLine();
                    Console.Write("Origem: ");
                    Posicao origem = Tela.lerPocicaoXadrez().toPosicao();
                    Console.Write("Destino: ");
                    Posicao destino = Tela.lerPocicaoXadrez().toPosicao();

                    partida.executaMovimento(origem, destino);
                }  

                
            }
            catch(TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }
           
            Console.ReadLine();

        }
    }
}

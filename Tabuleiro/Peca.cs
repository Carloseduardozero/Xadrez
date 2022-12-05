using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tabuleiro
{
     abstract class Peca
    {
        public Posicao posicao { get; set; }
        public Cor cor { get; protected set; }
        public int qteMovimento { get; protected set; }

        public Tabuleiro tab { get; protected set; }

        public  Peca( Cor cor, Tabuleiro tab)
        {
            this.posicao = null;
            this.cor = cor;
            this.qteMovimento = 0;
            this.tab = tab;

        }

        public abstract bool[,] movimentosPossiveis();

        public void incrementarQteMovimento() 
        {
            qteMovimento++;
        }

        public void decrementarQteMovimento()
        {
            qteMovimento--;
        }

        public bool existeMovimentosPossiveis() 
        {
            bool[,] mat = movimentosPossiveis();
            for (int i=0; i<tab.linhas; i++) 
            {
                for(int j=0; j<tab.colunas; j++) 
                {
                    if (mat[i, j]) 
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool movimentoPossivel(Posicao pos) 
        {
            return movimentosPossiveis()[pos.linha, pos.coluna];
        }

    }
}

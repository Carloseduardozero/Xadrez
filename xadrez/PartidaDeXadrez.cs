using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;

namespace xadrez
{
    class PartidaDeXadrez
    {
       public Tabuleiro tab { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; }

        private HashSet<Peca> pecas;

        private HashSet<Peca> capturada;

        public bool xeque { get; private set; }

        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            xeque = false;
            pecas = new HashSet<Peca>();
            capturada = new HashSet<Peca>();
            colocarPecas();
        }



        public Peca executaMovimento(Posicao origem , Posicao destino) 
        {
            Peca p = tab.retirarPeca(origem);
            p.incrementarQteMovimento();
            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.ColocarPeca(p, destino);
            if (pecaCapturada != null) 
            {
                capturada.Add(pecaCapturada);
            }
            return pecaCapturada;
        }

        public void desfazMovimeto(Posicao origem, Posicao destino, Peca pecaCapturada) 
        {
            Peca p = tab.retirarPeca(destino);
            p.decrementarQteMovimento();
            if (pecaCapturada != null) 
            {
                tab.ColocarPeca(pecaCapturada,destino);
                capturada.Remove(pecaCapturada);
            }
            tab.ColocarPeca(p, origem);
        }

        public void realizaJogada(Posicao origem, Posicao destino)
        {
             Peca pecaCapturada = executaMovimento(origem, destino);
            if (estaEmXeque(jogadorAtual)) 
            {
                desfazMovimeto(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque! ");
            }

            if (estaEmXeque(adversaria(jogadorAtual))) 
            {
                xeque = true;
            }
            else 
            {
                xeque = false;
            }

            turno++;
            mudaJogador();
        }

        public void validarPosicaoDeOrigem(Posicao pos)
        {
            if (tab.peca(pos) == null)
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida");
            }
            if (jogadorAtual != tab.peca(pos).cor) 
            {
                throw new TabuleiroException("A peça de origem  escolhida nao é sua!");
            }
            if (!tab.peca(pos).existeMovimentosPossiveis())
            {
                throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhido! ");
            }
        }

        public void validarPosicaoDeDestino(Posicao origem, Posicao destino) 
        {
            if (!tab.peca(origem).podeMoverPara(destino)) 
            {
                throw new TabuleiroException("Posoção de destino inválida! ");
            }
        }

        private void mudaJogador()
        {
            if(jogadorAtual == Cor.Branca) 
            {
                jogadorAtual = Cor.Preta;
            }
            else 
            {
                jogadorAtual = Cor.Branca;
            }
        }

        public HashSet<Peca> pecasCapturadas( Cor cor) 
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in capturada) 
            {
                if (x.cor == cor) 
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Peca> pecasEmJogo(Cor cor) 
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas)
            {
                if (x.cor == cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        } 

        private Cor adversaria(Cor cor) 
        {
            if (cor == Cor.Branca) 
            {
                return Cor.Preta;
            }
            else 
            {
                return Cor.Branca;
            }
        }

        private Peca rei(Cor cor) 
        {
            foreach (Peca x in pecasEmJogo(cor)) 
            {
                if (x is Rei) 
                {
                    return x;
                }
            }
            return null;
        }

        public bool estaEmXeque(Cor cor) 
        {
            Peca R = rei(cor);
            if(R == null) 
            {
                throw new TabuleiroException("Não tem rei desta cor " + cor);
            }
            foreach (Peca x in pecasEmJogo(adversaria(cor))) 
            {
                bool[,] mat = x.movimentosPossiveis();
                if (mat[R.posicao.linha, R.posicao.coluna]) 
                {
                    return true;
                }

            }
            return false;
        }

        public void colocarNovaPeca(char coluna, int linha, Peca peca) 
        {
            tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);
        }

        private void colocarPecas() 
        {
            colocarNovaPeca('c', 1, new Torre(Cor.Branca,tab));
            colocarNovaPeca('c', 2, new Torre(Cor.Branca, tab));
            colocarNovaPeca('d', 2, new Torre(Cor.Branca, tab));
            colocarNovaPeca('e', 2, new Torre(Cor.Branca, tab));
            colocarNovaPeca('e', 1, new Torre(Cor.Branca, tab));
            colocarNovaPeca('d', 1, new Rei(Cor.Branca, tab));

            colocarNovaPeca('c', 7, new Torre(Cor.Preta, tab));
            colocarNovaPeca('c', 8, new Torre(Cor.Preta, tab));
            colocarNovaPeca('d', 7, new Torre(Cor.Preta, tab));
            colocarNovaPeca('e', 7, new Torre(Cor.Preta, tab));
            colocarNovaPeca('e', 8, new Torre(Cor.Preta, tab));
            colocarNovaPeca('d', 8, new Rei(Cor.Preta, tab));

            //tab.ColocarPeca(new Torre(tab,Cor.Preta), new Posicao(0, 0));
            //tab.ColocarPeca(new Torre(Cor.Branca, tab), new PosicaoXadrez('c',1).toPosicao());


        }
    }
}

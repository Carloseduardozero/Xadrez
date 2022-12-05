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

        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            colocarPecas();
        }



        public void executaMovimento(Posicao origem , Posicao destino) 
        {
            Peca p = tab.retirarPeca(origem);
            p.incrementarQteMovimento();
            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.ColocarPeca(p, destino);
        }

        public void realizaJogada(Posicao origem, Posicao destino)
        {
            executaMovimento(origem, destino);
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

        private void colocarPecas() 
        {
            //tab.ColocarPeca(new Torre(tab,Cor.Preta), new Posicao(0, 0));
        tab.ColocarPeca(new Torre(Cor.Branca, tab), new PosicaoXadrez('c',1).toPosicao());
        tab.ColocarPeca(new Torre(Cor.Branca, tab), new PosicaoXadrez('c',2).toPosicao());
        tab.ColocarPeca(new Torre(Cor.Branca, tab), new PosicaoXadrez('d',2).toPosicao());
        tab.ColocarPeca(new Torre(Cor.Branca, tab), new PosicaoXadrez('e',2).toPosicao());
        tab.ColocarPeca(new Torre(Cor.Branca, tab), new PosicaoXadrez('e',1).toPosicao());
        tab.ColocarPeca(new Rei(Cor.Branca, tab), new PosicaoXadrez('d',1).toPosicao());

        tab.ColocarPeca(new Torre(Cor.Preta, tab), new PosicaoXadrez('c', 7).toPosicao());
        tab.ColocarPeca(new Torre(Cor.Preta, tab), new PosicaoXadrez('c', 8).toPosicao());
        tab.ColocarPeca(new Torre(Cor.Preta, tab), new PosicaoXadrez('d', 7).toPosicao());
        tab.ColocarPeca(new Torre(Cor.Preta, tab), new PosicaoXadrez('e', 7).toPosicao());
        tab.ColocarPeca(new Torre(Cor.Preta, tab), new PosicaoXadrez('e', 8).toPosicao());
        tab.ColocarPeca(new Rei(Cor.Preta, tab), new PosicaoXadrez('d', 8).toPosicao());

        }
    }
}

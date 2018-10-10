using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace FilosofosQueJantam
{
    class Filosofo
    {
        private bool[] garfos = new bool[2];
        private void SetGarfo(int lado,bool estado)
        {
            this.garfos[lado] = estado;
            this.SetLabel(0, lado, estado, this.sujos[lado]);
        }

        private bool[] tokens = new bool[2];

        private void SetToken(int lado, bool estado)
        {
            this.tokens[lado] = estado;
            this.SetLabel(1, lado, estado);
        }

        private bool[] sujos = new bool[2];

        private void SetSujo(int lado, bool estado)
        {
            this.sujos[lado] = estado;
            this.SetLabel(0, lado, this.garfos[lado], estado);
        }

        private int estado;

        private void SetEstado(int estado)
        {
            this.estado = estado;
        }

        private Filosofo[] filosofos;

        public Filosofo[] Filosofos
        {
            get { return filosofos; }
            set { filosofos = value; }
        }

        private PictureBox imagem = new PictureBox();

        private void SetImagem()
        {
            Bitmap imagem = null;
            switch (this.estado)
            {
                case 0:
                    imagem = FilosofosQueJantam.Properties.Resources.pensando;
                    break;
                case 1:
                    imagem = FilosofosQueJantam.Properties.Resources.faminto;
                    break;
                case 2:
                    imagem = FilosofosQueJantam.Properties.Resources.comendo;
                    break;
               
            }
            
            this.imagem.Image = imagem;
            
        }

        private Label[][] labels;

        private void SetLabel(int tipo, int lado, bool estado, bool sujo = false)
        {
            string text = "";
            if (estado)
            {
                if (tipo == 0)
                {
                    if (sujo)
                    {
                        text = "GS";
                    }
                    else
                    {
                        text = "GL";
                    }
                }
                else
                {
                    text = "T";
                }
            }
            this.labels[tipo][lado].Text = text;
            //this.labels[tipo][lado].Text = estado ? (tipo == 0 ? (sujo ? "GS" : "GL") : "T" ) : "";
        }

        private Button botao;
        public Filosofo(bool Garfo0, bool Garfo1, bool Token0, bool Token1, Label garfoD, Label garfoE, Label tokenD, Label tokenE, PictureBox imagem, Button botao)
        {
            this.labels = new Label[][] { new Label[] { garfoE, garfoD }, new Label[] { tokenE, tokenD } };
            this.SetSujo(0, Garfo0);
            this.SetSujo(1, Garfo1);
            this.SetGarfo(0,Garfo0);
            this.SetGarfo(1, Garfo1);
            this.SetToken(0, Token0);
            this.SetToken(1, Token1);
            this.estado = 0;
            this.imagem = imagem;
            this.SetImagem();
            this.botao = botao;
            this.botao.Text = "Pensando";

            this.Filosofos = new Filosofo[] { null, null };
        }

        private void PedindoGarfo(int lado)
        {
            if(this.estado == 1 && this.tokens[lado] && !this.garfos[lado])
            {
                this.SetToken(lado, false);
                this.Filosofos[lado].ReceberPedido(1 - lado);               
            }
        }

        private void LiberandoGarfo(int lado)
        {
            if (this.estado != 2 && this.tokens[lado] && this.sujos[lado])
            {
                this.SetSujo(lado,false);
                this.SetGarfo(lado,false);
                this.Filosofos[lado].ReceberGarfo(1 - lado);

            }
        }

        private void ReceberPedido(int lado)
        {
            this.SetToken(lado, true);
            this.LiberandoGarfo(lado);
            this.PedindoGarfo(lado);
        }

        private void ReceberGarfo(int lado)
        {
            this.SetSujo(lado, false);
            this.SetGarfo(lado, true);            
            this.Comer();

        }

        private void Comer()
        {
        
            if (this.estado == 1 && this.garfos[0] && this.garfos[1])
            {
                this.estado = 2;
                this.botao.Text = "Comendo";
                this.SetImagem();
                this.SetSujo(0, true);
                this.SetSujo(1, true);
                
            }
            else
            {
                this.PedindoGarfo(0);                
                this.PedindoGarfo(1);
            }

        }

        private void Pensar()
        {
            if (this.estado == 2)
            {
                this.estado = 0;
                this.botao.Text = "Pensando";
                this.SetImagem();
                this.LiberandoGarfo(0);
                this.LiberandoGarfo(1);
            }            
        }

        private void Faminto()
        {
            if (this.estado == 0)
            {
                this.estado = 1;
                this.botao.Text = "Faminto";
                this.SetImagem();
                this.Comer();
            }
                     
        }

        public void RealizarAcao()
        {
            switch (this.estado)
            {
                case 0:
                    this.Faminto();
                    break;
                case 1:
                   //Alerta 
                    break;
                case 2:
                    this.Pensar();
                    break;
            }
        }
    }
}

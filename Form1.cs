using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FilosofosQueJantam
{
    public partial class Form1 : Form
    {
        private Filosofo filosofo1;
        private Filosofo filosofo2;
        private Filosofo filosofo3;
       
        public Form1()
        {
            InitializeComponent();
            filosofo1 = new Filosofo(true, true, false, false,filosofo1GD,filosofo1GE,filosofo1TD,filosofo1TE,imagem1, button1);
            filosofo2 = new Filosofo(false, true, true, false, filosofo2GD, filosofo2GE, filosofo2TD, filosofo2TE, imagem2, button2);
            filosofo3 = new Filosofo(false, false, true, true, filosofo3GD, filosofo3GE, filosofo3TD, filosofo3TE, imagem3, button3);
            filosofo1.Filosofos[0] = filosofo3;
            filosofo1.Filosofos[1] = filosofo2;
            filosofo2.Filosofos[0] = filosofo1;
            filosofo2.Filosofos[1] = filosofo3;
            filosofo3.Filosofos[0] = filosofo2;
            filosofo3.Filosofos[1] = filosofo1;
            
        }

       
        private void button1_Click(object sender, EventArgs e)
        {
            this.filosofo1.RealizarAcao();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.filosofo2.RealizarAcao();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.filosofo3.RealizarAcao();
        }
        
    }
}

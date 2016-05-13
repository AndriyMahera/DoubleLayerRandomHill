using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace DoubleLayerRandomHill
{
    public partial class Form1 : Form
    {
        private List<int> random = new List<int>(),digit=new List<int>(),encrypted=new List<int>(),decrypted=new List<int>();
        private List<int> random2 = new List<int>(), encrypted2 = new List<int>(), decrypted2 = new List<int>();
        private Dictionary<string, int> dict = new Dictionary<string, int>();
        private string mainstring;
        private double[,] KeyMatrix = { { 12, 0, 7, 4 }, { 17, 14, 21, 18 }, { 10, 24, 0, 13 }, { 3, 17, 8, 24 } };
        private double[,] SKeyMatrix = { {1,2,1,7,1},{2,3,4,5,6},{1,2,8,3,4},{5,4,3,2,1},{6,7,5,4,2}};
        private double[,] InverseMatrix,SInverseMatrix;
        public  const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ .,;-'";

        public Form1()
        {
            InitializeComponent();
        }

        private void Perform_Click(object sender, EventArgs e)
        {
            mainstring = richTextBox1.Text.Length==0? File.ReadAllText("C_C_VT00"):richTextBox1.Text;
            Preparation.FilterText(ref mainstring);
            dict = Preparation.UniquesDict(mainstring,1);

            richTextBox1.Text = mainstring;
            digit = Preparation.FormDigitString(mainstring);
            encrypted = Code.HillPlusRandomEncrypt(digit,KeyMatrix,Alphabet.Length, random);
            richTextBox2.Text = Preparation.FormStringFromDigit(encrypted).ToString();


            Decrypt.Enabled = true;

            Preparation.ColorText(richTextBox1, random, KeyMatrix.GetLength(0),Color.Yellow);
            Preparation.ColorText(richTextBox2, random, KeyMatrix.GetLength(0), Color.Yellow);


            richTextBox3.Text = Preparation.FormStringFromDigit(encrypted).ToString();
            encrypted2 = Code.HillPlusRandomEncrypt(encrypted,SKeyMatrix,Alphabet.Length,random2);
            richTextBox4.Text = Preparation.FormStringFromDigit(encrypted2).ToString();

            Preparation.ColorText(richTextBox3, random2, SKeyMatrix.GetLength(0), Color.Blue);
            Preparation.ColorText(richTextBox4, random2, SKeyMatrix.GetLength(0), Color.Blue);
            
        }

        private void Decrypt_Click(object sender, EventArgs e)
        {
            //Decrypt
            double det = MathOperations.Determinant(KeyMatrix),det2=MathOperations.Determinant(SKeyMatrix);
            int ex = (int)det,ex2=(int)det2; int d,d2;
            try
            {
                int f = MathOperations.FindCoef(ex,256);
                d = MathOperations.FindCoef(ex, Alphabet.Length);
            }
            catch (ArgumentException)
            {
                int f = Math.Abs(MathOperations.FindCoef(-ex, 256) - 256);
                d = Math.Abs(MathOperations.FindCoef(-ex, Alphabet.Length) - Alphabet.Length);
            }
            try
            {
                d2 = MathOperations.FindCoef(ex2, Alphabet.Length);
            }
            catch (ArgumentException)
            {
                d2 = Math.Abs(MathOperations.FindCoef(-ex2, Alphabet.Length) - Alphabet.Length);
            }
            InverseMatrix = MathOperations.FormInverseMatrix(KeyMatrix, d, Alphabet.Length);
            SInverseMatrix = MathOperations.FormInverseMatrix(SKeyMatrix,d2,Alphabet.Length);
            decrypted2 = Code.HillPlusRandomDecrypt(encrypted2,SInverseMatrix,Alphabet.Length,random2);
            decrypted = Code.HillPlusRandomDecrypt(decrypted2, InverseMatrix, Alphabet.Length, random);

            richTextBox5.Text = Preparation.FormStringFromDigit(decrypted).ToString();
            
            
        }
    }
}

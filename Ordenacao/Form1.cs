using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Ordenacao
{
    public partial class Form1 : Form{
        public Form1(){
            InitializeComponent();
        }

        int tamanhoVetor = 0;
        int[] vetorAleatorio;
        int[] desorganizado;
        long tempo;
        int qtdExecucao = 100000;
        Stopwatch relogio = new Stopwatch();

        //Botão para gerar um vetor aleatório.
        private void button1_Click(object sender, EventArgs e)
        {
            Random randNum = new Random();
            randNum.Next();
            tamanhoVetor = int.Parse(tamanhoBox.Text);

            vetorAleatorio = new int[tamanhoVetor];

            for (int i = 0; i < tamanhoVetor; i++){
                vetorAleatorio[i] = randNum.Next(0, 500);
            }

            desorganizado = vetorAleatorio; //cópia de backup
            mostrarVetorOrdenado(vetorAleatorio);
            MessageBox.Show("Pronto!");
        }

        //Função para mostrar o vetor ordenado na tela.
        public void mostrarVetorOrdenado(int[] vetorImprimir){
            foreach (int numero in vetorImprimir){
                richTextBox1.AppendText(numero.ToString() + "\r\n"); richTextBox1.ScrollToCaret();
            }
        }

        //Função para mostrar o tempo na tela.
        public void mostrarTempo(){
            tempo = relogio.ElapsedMilliseconds; 
            relogio.Reset();
            MessageBox.Show("Tempo de Execução para " + qtdExecucao.ToString() + " vezes: " + tempo.ToString());
        }


        //Função para método Buble Sort.
        public int[] ordenarBuble(int[] desorganizado){
            for (int i = 0; i < tamanhoVetor; i++){
                for (int j = i + 1; j < tamanhoVetor; j++){
                    if (desorganizado[j] < desorganizado[i]){
                        int auxiliar = desorganizado[j];
                        desorganizado[j] = desorganizado[i];
                        desorganizado[i] = auxiliar;
                    }
                }
            }
            return desorganizado;//aqui já está organizado.
        }

        //Ordenar pelo método Buble Sort.
        private void button2_Click(object sender, EventArgs e){
            richTextBox1.Clear();
            int[] organizado = new int[tamanhoVetor];//variável local
            relogio.Start();
            for (int i=1;i< qtdExecucao; i++){
                organizado = ordenarBuble(vetorAleatorio);
            }
            relogio.Stop();

            //Mostrar o tempo de execução na tela.
            mostrarTempo();
            //Mostrar o vetor ordenado na tela.
            mostrarVetorOrdenado(organizado);
        }

        //Função para o método Select Sort
        public int[] ordenarSelect(int[] desorganizado){
            for (int i = 0; i < tamanhoVetor; i++){
                int menor = desorganizado[i];
                int posicaoMenor = i;
                for (int j = i + 1; j < tamanhoVetor; j++){
                    if (desorganizado[j] < menor){
                        menor = desorganizado[j];
                        posicaoMenor = j;
                    }
                    desorganizado[posicaoMenor] = desorganizado[i];
                    desorganizado[i] = menor;
                }
            }
            return desorganizado;
        }

        //Ordenar pelo método Select Sort.
        private void button3_Click(object sender, EventArgs e){
            richTextBox1.Clear();
            int[] organizado = new int[tamanhoVetor];//variável local
            relogio.Start();
            for (int i = 1; i < qtdExecucao; i++){
                organizado = ordenarSelect(vetorAleatorio);
            }
            relogio.Stop();

            //Mostrar o tempo de execução na tela.
            mostrarTempo();
            //Mostrar o vetor ordenado na tela.
            mostrarVetorOrdenado(organizado);
        }

        //Função para o método Insertion Sort.
        public int[] ordenarInsertion(int[] desorganizado){
            int i, j, atual;
            for (i = 1; i < tamanhoVetor; i++){
                atual = desorganizado[i];
                j = i;
                while ((j > 0) && (desorganizado[j - 1] > atual)){
                    desorganizado[j] = desorganizado[j - 1];
                    j = j - 1;
                }
                desorganizado[j] = atual;
            }
            return desorganizado;
        }

        //Ordenar pelo método Insertion Sort.
        private void button4_Click(object sender, EventArgs e){
            richTextBox1.Clear();
            int[] organizado = new int[tamanhoVetor];//variável local
            relogio.Start();
            for (int i = 1; i < qtdExecucao; i++){
                organizado = ordenarInsertion(vetorAleatorio);
            }
            relogio.Stop();

            //Mostrar o tempo de execução na tela.
            mostrarTempo();
            //Mostrar o vetor ordenado na tela.
            mostrarVetorOrdenado(organizado);
        }

        //Função principal para o método Heap Sort.
        public int[] ordenarHeap(int[] desorganizado){
            buildMaxHeap(desorganizado);
            int n = desorganizado.Length;
            for (int i = desorganizado.Length - 1; i > 0; i--){
                swap(desorganizado, i, 0);
                maxHeapify(desorganizado, 0, --n);

            }
            return desorganizado;
        }

        //Ordenar pelo método Heap Sort.
        private void button5_Click(object sender, EventArgs e){
            richTextBox1.Clear();
            int[] organizado = new int[tamanhoVetor];//variável local
            relogio.Start();
            for (int i = 1; i < qtdExecucao; i++){
                organizado = ordenarHeap(vetorAleatorio);
            }
            relogio.Stop();

            //Mostrar o tempo de execução na tela.
            mostrarTempo();
            //Mostrar o vetor ordenado na tela.
            mostrarVetorOrdenado(organizado);
        }

        //Funções necessárias para o método Heap Sort.
        private static void buildMaxHeap(int[] v){
            for (int i = v.Length / 2 - 1; i >= 0; i--){
                maxHeapify(v, i, v.Length);
            }
        }

        private static void maxHeapify(int[] v, int pos, int n){
            int max = 2 * pos + 1, right = max + 1;
            if (max < n){
                if (right < n && v[max] < v[right]){
                    max = right;
                }
                if (v[max] > v[pos]){
                    swap(v, max, pos);
                    maxHeapify(v, max, n);
                }
            }
        }

        private static void swap(int[] v, int j, int aposJ){
            int aux = v[j];
            v[j] = v[aposJ];
            v[aposJ] = aux;
        }


    }
}

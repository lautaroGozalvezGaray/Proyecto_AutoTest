using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SP3
{
    public partial class Form1 : Form
    {
        // declaración de la estructura para los turnos
        public struct TURNO
        {
            public int NumeroTurno;
            public string Dominio;
            public int AnioFabricacion;
            public string Titular;
        };

        // constante para la cantidad total de elementos del arreglo
        const int MAX = 50;
        // declaración del arreglo unidimensional de 50 elementos
        public TURNO[] turnos;
        // variable para controlar la cantidad de elementos cargados
        public int Cantidad = 0;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // creación del arreglo
            turnos = new TURNO[MAX];
            // inicializar la variable que controla la cantidad de elementos cargados
            Cantidad = 0;
            // establecer el estado inicial de todos los componentes de la interfaz
            LimpiarInterfaz();
            // limpiar la grilla
            dgvTurnos.Rows.Clear();
        }

        

        // Evento KeyPress: convierte minúsculas en mayúsculas y no permite
        // ingresar más que letras y números ( y borrar)
        private void txtDominio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(Char.IsLower(e.KeyChar) == true) // es una letra minúscula ??
            {
                e.KeyChar = Char.ToUpper(e.KeyChar); // la convierte en la mayúscula correspondiente
            }
            if(Char.IsLetterOrDigit(e.KeyChar) == false && e.KeyChar != (char)Keys.Back )
            {
                // si la tecla no es ni letra ni numero y es distinta de backspace se anula
                e.Handled = true;
            }
        }

        // Evento KeyPress: debe permitir ingresar solamente números (y borrar)
        private void txtNroTurno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != (char)Keys.Back )
            {
                e.Handled = true;
            }
            // también se puede resolver usando le objeto 'Char' y el método isLetter()', quedaría así:
            //if( Char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back )
            //{
            //    e.Handled = true;
            //}
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            // validar los datos antes de ingresarlos al arreglo
            if(ValidarDatos() == true )
            {
                // asignar los valores a cada campo del arreglo, en la posición indicada por la variable Cantidad
                int nro = int.Parse(txtNroTurno.Text); // obtener el número de turno que se quiere registrar
                turnos[Cantidad].NumeroTurno = nro;
                turnos[Cantidad].Dominio = txtDominio.Text;
                turnos[Cantidad].AnioFabricacion =
                            int.Parse(numAnioFabricacion.Value.ToString());
                turnos[Cantidad].Titular = txtTitular.Text;
                // incrementar la cantidad de elementos cargados
                Cantidad++;
                // agregar los datos ingresados a la grilla
                dgvTurnos.Rows.Add(txtNroTurno.Text, txtDominio.Text, numAnioFabricacion.Value.ToString(), txtTitular.Text);
                // restaurar el estado inicial del formulario
                LimpiarInterfaz();
            }
        }

        // Función 'ValidarDatos', controla que los datos a ingresar sean correctos y devuelve verdadero
        // en caso contrario devuelve falso
        private bool ValidarDatos()
        {
            bool resultado = false; // valor a devolver si no se cumplen todas las validaciones

            if (Cantidad < MAX) // hay lugar libre en el arreglo ?
            {
                if (txtNroTurno.Text != "" && txtDominio.Text.Length >= 6 && txtTitular.Text.Length >= 2) // validar el contenido del Turno, Dominio y Titular
                {
                    int nro = int.Parse(txtNroTurno.Text); // obtener el número de turno que se quiere registrar
                    if (BuscarTurno(nro) == false) // controlar si ese número de turno ya fue cargado, BuscarTurno devuelve falso si el numero no está y verdadero en caso contrario
                    {
                        // si llega acá es porque todas las condiciones para cargar un turno se cumplen
                        resultado = true; // ValidarDatos() devolverá verdadero
                    }
                    else
                    {
                        MessageBox.Show("El número de turno ya está registrado", "ERROR",
                                         MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Debe completar los datos faltantes", "ERROR",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No hay espacio disponible para registrar más turnos", "ERROR",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // devuelve el valor asignado en la variable 'resultado'
            return resultado;
        }

        // Función 'BuscarTurno', recibe por parámetro el número de turno a buscar en el arreglo
        private bool BuscarTurno(int nro)
        {
            bool resultado = false; // falso será el valor a devolver si el número de turno no está almacenado en el arreglo
            int i;
            // recorrer con el valor de 'i' el arreglo desde 0 hasta la cantidad cargada
            for(i=0; i < Cantidad; i++)
            {
                // comparar el número de turno buscado con el número de turno almacenado en el arreglo
                if(nro == turnos[i].NumeroTurno)
                {
                    resultado = true; // si lo encuentra devuelve verdadero
                    break; // sale del ciclo 'for'
                }
            }
            // devuleve el valor asignado en la variable 'resultado'
            return resultado;
        }

        // Función 'LimpiarInterfaz', coloca los componenetes en su estado inicial
        private void LimpiarInterfaz()
        {
            txtNroTurno.Text = "";
            txtDominio.Text = "";
            txtTitular.Text = "";
            
        }
        

        // botón Salir
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close(); // cierra el formulario
        }

        // botón Consultar
        private void btnConsultar_Click(object sender, EventArgs e)
        {
            // limpiar la grilla
            dgvConsulta.Rows.Clear();

            // primer consulta: cantidad de turnos registrados
            // se obtiene directamente de la variable 'Cantidad': el índice usado para la carga del arreglo almacena la cantidad de elementos cargados
            txtCantidadTurnos.Text = Cantidad.ToString(); //se convierte a string para asignar a la propiedad Text del TextBox

        }

        private void btnConsultar2_Click(object sender, EventArgs e)
        {
            // limpiar la grilla
            dgvConsulta.Rows.Clear();
            
            // segunda consulta: año del vehículo más antiguo registrado
            // se debe recorrer el arreglo y con una variable auxiliar comparar el año del modelo en cada turno
            int menor = int.Parse(numAnioFabricacion.Maximum.ToString()); // se toma como valor inicial el mayor valor posible
            int i;
            int posMenor = 0; // guarda la posición del menor valor
            for (i = 0; i < Cantidad; i++) // se recorre el arreglo hasta la posición que tiene datos cargados, no es ncesario recorrer el arreglo completo
            {
                // comparar el campo del año de fabricación en cada turno con la variable auxiliar (menor)
                if (turnos[i].AnioFabricacion < menor)
                {
                    menor = turnos[i].AnioFabricacion; // se guarda en la variable el valor que sea menor 
                    posMenor = i; // guardamos la posición que ocupa el menor valor para después poder recuperar todos los datos de esa posisición
                }
            }
            // al terminar de recorrer el arreglo se muestra el valor obtenido en la variable 'menor':
            txtAnioMasAntiguo.Text = menor.ToString();
            // cargar la grilla con los datos del turno que contiene el año más antiguo (valor guardado en posMenor)
            dgvConsulta.Rows.Add(turnos[posMenor].NumeroTurno.ToString(),
                                  turnos[posMenor].Dominio,
                                  turnos[posMenor].AnioFabricacion.ToString(),
                                  turnos[posMenor].Titular);
        }

        private void btnConsultar3_Click(object sender, EventArgs e)
        {
            // tercer consulta: cantidad de turnos con patente de 6 caracteres
            // usamos una variable como contador de los turnos que cumplen con esa condición
            // limpiar la grilla
            dgvConsulta.Rows.Clear();
            int i;
            int contador = 0;
            // recorrer el arreglo de turnos
            for (i = 0; i < Cantidad; i++)
            {
                // controlar la cantidad de caracteres del dominio en cada turno
                if (turnos[i].Dominio.Length == 6) // la propiedad 'Length' contiene la cantidad de caracteres
                {
                    contador++; // incrementar la cantidad de turnos con la condición de 6 caracteres
                    // cargar la grilla con los datos del turno con dominio de 6 caracteres
                    dgvConsulta.Rows.Add(turnos[i].NumeroTurno.ToString(),
                                  turnos[i].Dominio,
                                  turnos[i].AnioFabricacion.ToString(),
                                  turnos[i].Titular);
                }
            }
            // mostrar el resultado
            txtDominio6Caracteres.Text = contador.ToString();
        }
    }
}

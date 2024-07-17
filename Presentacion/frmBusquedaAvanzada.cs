using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class frmBusquedaAvanzada : Form
    {
        CategoriaDatos categoria;
        MarcaDatos marca;
        DataGridView listadoArticulos;


        public frmBusquedaAvanzada(DataGridView listado)
        {
            InitializeComponent();
            this.listadoArticulos = listado;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();

        }

        private void frmBusquedaAvanzada_Load(object sender, EventArgs e)
        {
           
            cbxCriterio.Items.Add("Marca");
            cbxCriterio.Items.Add("Categoría");
            cbxCriterio.Items.Add("Precio");

        }

        private void cbxCriterio_SelectedIndexChanged(object sender, EventArgs e)
        {

            string opcion = cbxCriterio.SelectedItem.ToString();
            try
            {
                if (opcion == "Precio")
                {
                    List <string> rango = new List<string> ();
                    rango.Add("Menor a");
                    rango.Add("Mayor a");
                    rango.Add("Igual a");
                    cbxSubcriterio.DataSource = rango;
                    
                }
                else if (opcion == "Categoría")
                {
                    categoria = new CategoriaDatos();
                    cbxSubcriterio.DataSource = categoria.listar();
                    cbxSubcriterio.ValueMember = "Id";
                    cbxSubcriterio.DisplayMember = "Descripcion";

                }
                else
                {
                    marca = new MarcaDatos();
                    cbxSubcriterio.DataSource = marca.listar();
                    cbxSubcriterio.ValueMember = "Id";
                    cbxSubcriterio.DisplayMember = "Nombre";
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
            
        }
        private bool validarFiltro()
        {
            if (cbxCriterio.SelectedIndex < 0 && cbxSubcriterio.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione criterio y subcriterio para poder buscar.");
                return true;
            }
            if (string.IsNullOrEmpty(txtParametro.Text))
            {
                MessageBox.Show("El parámetro de búsqueda no puede estar vacío.");
                return true;
            }    
            
            if (cbxCriterio.SelectedItem.ToString() == "Precio")
            {

                if (!(validarNumeros(txtParametro.Text)))
                {
                    MessageBox.Show("Ingrese sólo números.");
                    return true;
                }
                    
            }

            

            return false;
        }

        private bool validarNumeros(string numero)
        {
            foreach (char n in numero)
            {
                if(!(char.IsNumber(n)))
                    return false;
            }
            return true;
        }
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            ArticuloDatos articulos = new ArticuloDatos();
            
            try
            {
                if (validarFiltro())
                    return;
                string criterio = cbxCriterio.SelectedItem.ToString();
                string subcriterio = cbxSubcriterio.SelectedItem.ToString();
                string buscado = txtParametro.Text;
                
                
                listadoArticulos.DataSource = articulos.filtrar(criterio, subcriterio, buscado);
                Close();
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }


        }
    }
}

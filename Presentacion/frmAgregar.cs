using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocio;
using Dominio;

namespace Presentacion
{
    public partial class frmAgregar : Form
    {
        public frmAgregar()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtImg_TextChanged(object sender, EventArgs e)
        {
            pbxImg.Load(txtImg.Text);
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Articulo art = new Articulo();
            ArticuloDatos datos = new ArticuloDatos();
            try
            {
                art.Codigo = txtCodigo.Text;
                art.Nombre = txtNombre.Text;
                art.Descripcion = txtDescripcion.Text;
                art.Precio = decimal.Parse(txtPrecio.Text);
                art.UrlImagen = txtImg.Text;
                art.Categoria = (Categoria)cbxCategoria.SelectedItem;
                art.Marca = (Marca)cbxMarca.SelectedItem;

                datos.agregarArticulo(art);
                MessageBox.Show("Agregado exitosamente.");
                
                Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void frmAgregar_Load(object sender, EventArgs e)
        {
            CategoriaDatos categoria = new CategoriaDatos();
            MarcaDatos marca = new MarcaDatos();
            try
            {
                cbxCategoria.DataSource = categoria.listar();
                cbxMarca.DataSource = marca.listar();
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
    }
}

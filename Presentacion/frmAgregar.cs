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
        private Articulo art = null;
        public frmAgregar()
        {
            InitializeComponent();
        }
        public frmAgregar(Articulo articulo)
        {
            InitializeComponent();
            this.art = articulo;
            Text = "Modificar artículo";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtImg_TextChanged(object sender, EventArgs e)
        {
            
            try
            {
            pbxImg.Load(txtImg.Text);

            }
            catch (Exception)
            {

                pbxImg.Load("https://archive.org/download/placeholder-image/placeholder-image.jpg");
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            
            ArticuloDatos datos = new ArticuloDatos();
            try
            {
                if(art  == null)
                    art = new Articulo();
                art.Codigo = txtCodigo.Text;
                art.Nombre = txtNombre.Text;
                art.Descripcion = txtDescripcion.Text;
                art.Precio = decimal.Parse(txtPrecio.Text);
                art.UrlImagen = txtImg.Text;
                art.Categoria = (Categoria)cbxCategoria.SelectedItem;
                art.Marca = (Marca)cbxMarca.SelectedItem;

                if (art.Id != 0)
                {
                    datos.modificarArticulo(art);
                    MessageBox.Show("Cambios realizados exitosamente.");
                }
                else {
                    datos.agregarArticulo(art);
                    MessageBox.Show("Agregado exitosamente.");
                }
                
                

                
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
                cbxCategoria.ValueMember = "Id" ;
                cbxCategoria.DisplayMember = "Descripcion";
                cbxMarca.DataSource = marca.listar();
                cbxMarca.ValueMember = "Id";
                cbxMarca.DisplayMember = "Nombre";

                if (art != null) {
                    txtCodigo.Text = art.Codigo;
                    txtDescripcion.Text = art.Descripcion;
                    txtNombre.Text = art.Nombre;
                    txtPrecio.Text = art.Precio.ToString();
                    txtImg.Text = art.UrlImagen;
                    cbxCategoria.SelectedValue = art.Categoria.Id;
                    cbxMarca.SelectedValue = art.Marca.Id;


                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
    }
}

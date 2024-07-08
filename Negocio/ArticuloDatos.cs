using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ArticuloDatos
    {

        public List<Articulo> listar()
        {
            List<Articulo> listado = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("select A.Id, Codigo, Nombre, A.Descripcion, M.Descripcion Marca, C.Descripcion Categoria, ImagenUrl, Precio, A.IdCategoria, A.IdMarca from ARTICULOS A, CATEGORIAS C, MARCAS M where A.IdMarca= M.Id and A.IdCategoria=C.Id");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo articulo = new Articulo();

                    articulo.Id = (int)datos.Lector["Id"];
                    articulo.Codigo = datos.Lector.IsDBNull(datos.Lector.GetOrdinal("Codigo")) ? null : (string)datos.Lector["Codigo"];
                    articulo.Nombre = datos.Lector.IsDBNull(datos.Lector.GetOrdinal("Nombre")) ? null : (string)datos.Lector["Nombre"];
                    articulo.Descripcion = datos.Lector.IsDBNull(datos.Lector.GetOrdinal("Descripcion")) ? null : (string)datos.Lector["Descripcion"];

                    articulo.Marca = new Marca();
                    articulo.Marca.Id = (int)datos.Lector["IdMarca"];
                    articulo.Marca.Nombre = datos.Lector.IsDBNull(datos.Lector.GetOrdinal("Marca")) ? null : (string)datos.Lector["Marca"];

                    articulo.Categoria = new Categoria();
                    articulo.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    articulo.Categoria.Descripcion = datos.Lector.IsDBNull(datos.Lector.GetOrdinal("Categoria")) ? null : (string)datos.Lector["Categoria"];

                    articulo.UrlImagen = datos.Lector.IsDBNull(datos.Lector.GetOrdinal("ImagenUrl")) ? null : (string)datos.Lector["ImagenUrl"];

                    
                    if (!(datos.Lector["Precio"]is DBNull))
                    articulo.Precio = (decimal)datos.Lector["Precio"];

                    listado.Add(articulo);

                }

                return listado;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {

                datos.cerrarConexion();
            }
        }

        public void agregarArticulo(Articulo nuevoArticulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("insert into ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, ImagenUrl, Precio)values(@CodigoArt, @Nombre, @Descripcion,@IdMarca, @IdCategoria,@ImgUrl, @Precio");
                datos.setearParametros("@CodigoArt", nuevoArticulo.Codigo);
                datos.setearParametros("@Nombre", nuevoArticulo.Nombre);
                datos.setearParametros("@Descripcion", nuevoArticulo.Descripcion);
                datos.setearParametros("@IdMarca", nuevoArticulo.Marca.Id);
                datos.setearParametros("@IdCategoria", nuevoArticulo.Categoria.Id);
                datos.setearParametros("@ImUrl", nuevoArticulo.UrlImagen);
                datos.setearParametros("@Precio", nuevoArticulo.Precio);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { datos.cerrarConexion(); }
        }

        public void modificarArticulo(Articulo articulo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("update ARTICULOS set Codigo=@codigo, Nombre=@nombre, Descripcion=@descripcion, IdMarca=@IdMarca, IdCategoria=@IdCat, ImagenUrl=@UrlImg, Precio=@precio where Id=@id");
                datos.setearParametros("@codigo", articulo.Codigo);
                datos.setearParametros("@nombre", articulo.Nombre);
                datos.setearParametros("@descripcion", articulo.Descripcion);
                datos.setearParametros("@IdMarca", articulo.Marca.Id);
                datos.setearParametros("@IdCat", articulo.Categoria.Id);
                datos.setearParametros("@UrlImg", articulo.UrlImagen);
                datos.setearParametros("@precio", articulo.Precio);
                datos.setearParametros("@id", articulo.Id);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { datos.cerrarConexion(); }
        }
    }
}
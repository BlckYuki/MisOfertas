﻿using CapaConexion;
using CapaDTO;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class NegocioProducto
    {
        private Conexion conexion = new Conexion();
        OracleConnection conn;
        public NegocioProducto()
        {
            conn = conexion.conectar();
        }

        public Producto retornaProducto(int id)
        {
            Producto producto = new Producto();
            conn.Open();

            DataSet ds = new DataSet();
            OracleCommand cmd = new OracleCommand();
            cmd = new OracleCommand("SELECT * FROM producto where idProducto=:id", conn);
            cmd.Parameters.Add(new OracleParameter(":id", id));

            OracleDataAdapter da = new OracleDataAdapter();
            da.SelectCommand = cmd;
            OracleDataReader dr = cmd.ExecuteReader();     

            
            while (dr.Read())
            {

                producto.IdProducto = dr.GetInt32(0);
                producto.Marca = String.Format("{0}", dr[1]);
                producto.Descripcion = String.Format("{0}", dr[2]);
                producto.Precio = dr.GetInt32(3);
                producto.TipoProducto.IdTipoProducto = dr.GetInt32(4);
                producto.Sucursal.IdSucursal = dr.GetInt32(5);

           }

            conn.Close();

            return producto;
        }

        public List<TipoProducto> retornaTipoProducto()
        {
            List<TipoProducto> list = new List<TipoProducto>();
            conn.Open();

            DataSet ds = new DataSet();
            OracleCommand cmd = new OracleCommand();
            cmd = new OracleCommand("SELECT * FROM tipoProducto", conn);
            

            OracleDataAdapter da = new OracleDataAdapter();
            da.SelectCommand = cmd;
            OracleDataReader dr = cmd.ExecuteReader();


            while (dr.Read())
            {
                TipoProducto tipoProducto = new TipoProducto();
                tipoProducto.IdTipoProducto = dr.GetInt32(0);
                tipoProducto.Nombre = String.Format("{0}", dr[1]);                
                list.Add(tipoProducto);
            }

            conn.Close();

            return list;
        }

        public List<Sucursal> retornaSucursal()
        {
            List<Sucursal> list = new List<Sucursal>();
            conn.Open();

            DataSet ds = new DataSet();
            OracleCommand cmd = new OracleCommand();
            cmd = new OracleCommand("SELECT * FROM sucursal", conn);


            OracleDataAdapter da = new OracleDataAdapter();
            da.SelectCommand = cmd;
            OracleDataReader dr = cmd.ExecuteReader();


            while (dr.Read())
            {
                Sucursal sucursal = new Sucursal();

                sucursal.IdSucursal = dr.GetInt32(0);
                sucursal.Nombre = String.Format("{0}", dr[1]);
                sucursal.Direccion = String.Format("{0}", dr[2]);
                sucursal.Telefono = dr.GetInt32(3);
                sucursal.Comuna.idComuna = dr.GetInt32(4);

                list.Add(sucursal);
            }

            conn.Close();

            return list;
        }

        public bool insertarProducto(Producto producto)
        {
            bool resultado = false;

            conn.Open();

            OracleCommand cmd = new OracleCommand("INSERT INTO producto(idProducto,nombre," +
                "descripcion,precio,idTipoProducto,idSucursal) VALUES (sucuence_producto.NEXTVAL," +
                ":nombre,:descripcion,:precio,:idTipoP,:sucursal)", conn);

            cmd.Parameters.Add(new OracleParameter(":nombre", producto.Marca));
            cmd.Parameters.Add(new OracleParameter(":descripcion", producto.Descripcion));
            cmd.Parameters.Add(new OracleParameter(":precio", producto.Precio));
            cmd.Parameters.Add(new OracleParameter(":idTipoP", producto.TipoProducto.IdTipoProducto));
            cmd.Parameters.Add(new OracleParameter(":sucursal", producto.Sucursal.IdSucursal));

            int a = cmd.ExecuteNonQuery();
            conn.Close();
            if (a > 0)
            {
                resultado = true;
            }

            return resultado;

        }


        public bool eliminarProducto(int id)
        {
            bool resultado = false;

            conn.Open();
            OracleCommand cmd = new OracleCommand("DELETE from producto where Idproducto =:idProducto", conn);

            cmd.Parameters.Add(new OracleParameter(":idProducto", id));

            int a = cmd.ExecuteNonQuery();
            conn.Close();
            if (a > 0)
            {
                resultado = true;
            }

            return resultado;

        }


        public bool actualizarProducto(Producto producto)
        {
            bool resultado = false;
                      

            conn.Open();

            OracleCommand cmd = new OracleCommand("UPDATE producto SET  marca ='" + producto.Marca + "', modelo='" + producto.Modelo + "', descripcion='" + producto.Descripcion + "', " +
           "precio='" + producto.Precio + "', stock='" + producto.Stock + "' WHERE idProducto ='" + producto.IdProducto + "'", conn);

            cmd.Parameters.Add(new OracleParameter(":marca", producto.Marca));
            cmd.Parameters.Add(new OracleParameter(":modelo", producto.Modelo));
            cmd.Parameters.Add(new OracleParameter(":descripcion", producto.Descripcion));
            cmd.Parameters.Add(new OracleParameter(":precio", producto.Precio));
            cmd.Parameters.Add(new OracleParameter(":stock", producto.Stock));



            int a = cmd.ExecuteNonQuery();
            conn.Close();
            if (a > 0)
            {
                resultado = true;
            }

            return resultado;

        }

        public List<Producto> retornaProductoList()
        {
            List<Producto> list = new List<Producto>();

            conn.Open();

            DataSet ds = new DataSet();
            OracleCommand cmd = new OracleCommand();
            cmd = new OracleCommand("SELECT * FROM producto", conn);


            OracleDataAdapter da = new OracleDataAdapter();
            da.SelectCommand = cmd;
            OracleDataReader dr = cmd.ExecuteReader();


            while (dr.Read())
            {

                Producto producto = new Producto();
                producto.IdProducto = dr.GetInt32(0);
                producto.Marca = String.Format("{0}", dr[1]);
                producto.Modelo=String.Format("{0}", dr[2]);
                producto.Descripcion = String.Format("{0}", dr[3]);
                producto.Precio = dr.GetInt32(4);
                producto.TipoProducto.IdTipoProducto = dr.GetInt32(5);
                producto.Sucursal.IdSucursal = dr.GetInt32(6);
                

                list.Add(producto);
            }

            conn.Close();

            return list;
        }


    }
}

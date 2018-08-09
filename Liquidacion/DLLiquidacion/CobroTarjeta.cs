// Decompiled with JetBrains decompiler
// Type: SigametLiquidacion.Pedido
// Assembly: DLLiquidacion, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7D22AA6E-AE05-4F4C-8F60-FC6A8FDBE6F8
// Assembly location: C:\Proyectos\SigametLiquidacion\DLLiquidacion.dll

using DocumentosBSR;
using System;

namespace SigametLiquidacion
{
    [Serializable]
    public class CobroTarjeta
    {
        private int _banco;
        private string _autorizacion;
        private string _tarjeta;
        private bool _encontrado;

        public bool Encontrado
        {
            get
            {
                return _encontrado;
            }

            set
            {
                _encontrado = value;
            }
        }

        public CobroTarjeta(int Banco, string Autorizacion, string Tarjeta)
        {
            this._banco = Banco;
            this._autorizacion = Autorizacion;
            this._tarjeta = Tarjeta;
        }

       

        public void consulta()
        {
            DatosCobroTarjeta objDatos = new DatosCobroTarjeta(_banco, _autorizacion, _tarjeta);

            _encontrado = objDatos.consulta();
        }

       
    }
}

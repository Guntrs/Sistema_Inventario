using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SistemaFacturacion.Entidades.Sucursal;
using System;
using System.Collections.Generic;
using System.Text;
using SistemaFacturacion.Entidades.Factura;

namespace SistemaFacturacion.Datos.Mapping.Factura
{
    public class BitacoraEnvioMap : IEntityTypeConfiguration<BitacoraEnvio>
    {
        public void Configure(EntityTypeBuilder<BitacoraEnvio> builder)
        {
            builder.ToTable("BitacoraEnvio")
               .HasKey(u => u.IdBitacora);
        }
    }
}

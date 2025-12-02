using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaFacturacion.Entidades.Ventas;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaFacturacion.Datos.Mapping.Ventas
{
    public  class VentaMap : IEntityTypeConfiguration<Venta>
    {
        public void Configure(EntityTypeBuilder<Venta> builder)
        {
            builder.ToTable("Venta")
                .HasKey(v => v.IdVenta);


            builder.HasOne(v => v.persona)
                .WithMany(p => p.ventas)
                .HasForeignKey(v => v.IdCliente);
        }
    }
}

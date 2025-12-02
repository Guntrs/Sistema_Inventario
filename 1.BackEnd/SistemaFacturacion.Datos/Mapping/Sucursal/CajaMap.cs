using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SistemaFacturacion.Entidades.Almacen;
using SistemaFacturacion.Entidades.Sucursal;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaFacturacion.Datos.Mapping.Sucursal
{
    public class CajaMap : IEntityTypeConfiguration<Caja>
    {

            public void Configure(EntityTypeBuilder<Caja> builder)
            {
                builder.ToTable("Caja")
                   .HasKey(u => u.IdCaja);
            }

        
    }
}

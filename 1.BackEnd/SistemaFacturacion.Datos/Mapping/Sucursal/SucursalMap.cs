using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaFacturacion.Entidades.Sucursal;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaFacturacion.Datos.Mapping.Sucursal
{
    public class SucursalMap :IEntityTypeConfiguration<Sucursal1>
    {
        public void Configure(EntityTypeBuilder<Sucursal1> builder)
        {
            builder.ToTable("Sucursal")
               .HasKey(u => u.IdSucursal);
        }
    }
}

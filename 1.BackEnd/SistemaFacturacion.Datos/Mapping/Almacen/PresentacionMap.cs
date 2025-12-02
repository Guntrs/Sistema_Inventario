using SistemaFacturacion.Entidades.Almacen;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaFacturacion.Datos.Mapping.Almacen
{
    public class PresentacionMap : IEntityTypeConfiguration<Presentacion>
    {
        public void Configure(EntityTypeBuilder<Presentacion> builder)
        {
            builder.ToTable("Presentacion")
                .HasKey(c => c.IdPresentacion);           
        }


    }
}

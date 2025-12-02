using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaFacturacion.Entidades.Ventas;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaFacturacion.Datos.Mapping.Ventas
{
    public class PersonaMap : IEntityTypeConfiguration<Persona>
    {
        public void Configure(EntityTypeBuilder<Persona> builder)
        {
            builder.ToTable("Persona")
               .HasKey(u => u.IdPersona);
        }

    }
}

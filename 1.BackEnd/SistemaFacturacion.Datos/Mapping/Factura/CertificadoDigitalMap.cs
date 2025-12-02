using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SistemaFacturacion.Entidades.Factura;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaFacturacion.Datos.Mapping.Factura
{
    public class CertificadoDigitalMap : IEntityTypeConfiguration<CertificadoDigital>
    {
        public void Configure(EntityTypeBuilder<CertificadoDigital> builder)
        {
            builder.ToTable("CertificadoDigital")
               .HasKey(u => u.IdCertificado);
        }
    }
}
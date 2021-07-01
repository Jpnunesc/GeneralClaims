using Domain.Entitys;
using Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Mappings
{
    [DbContext(typeof(GeneralClaimsContext))]
    public class FavoritoMap
    {
        public FavoritoMap(EntityTypeBuilder<FavoritosEntity> builder)
        {
            builder.ToTable("Favorito");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                   .HasColumnName("Id");

            builder.Property(t => t.IdFavorito)
                   .HasColumnName("IdFavorito");
        }
    }
}


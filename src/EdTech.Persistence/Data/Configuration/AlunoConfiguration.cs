using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EdTech.Core.Entities;

namespace SACA.Data.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Aluno>
    {
        public void Configure(EntityTypeBuilder<Aluno> builder)
        {
            builder.HasKey(x => x.RA);

            builder.Property(x => x.RA)
                .HasMaxLength(20);

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.OwnsOne(x => x.Cpf, cpf =>
            {
                cpf.WithOwner();

                cpf.Property(x => x.Codigo)
                    .HasMaxLength(14)
                    .IsRequired();
            })
                .Navigation(x => x.Cpf)
                .IsRequired();
        }
    }
}
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ProjectMann.Core.Domain;

#nullable disable

namespace ProjectMann.Infrastructure.Data
{
    public partial class ProjectMannDbContext : DbContext
    {
        public ProjectMannDbContext()
        {
        }

        public ProjectMannDbContext(DbContextOptions<ProjectMannDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Comentario> Comentarios { get; set; }
        public virtual DbSet<ComentarioItemTrabajo> ComentarioItemTrabajos { get; set; }
        public virtual DbSet<ComentarioProyecto> ComentarioProyectos { get; set; }
        public virtual DbSet<ComentarioTicket> ComentarioTickets { get; set; }
        public virtual DbSet<Estado> Estados { get; set; }
        public virtual DbSet<ItemInventario> ItemInventarios { get; set; }
        public virtual DbSet<ItemTrabajo> ItemTrabajos { get; set; }
        public virtual DbSet<ItemTrabajoEnlace> ItemTrabajoEnlaces { get; set; }
        public virtual DbSet<ItemTrabajoProyecto> ItemTrabajoProyectos { get; set; }
        public virtual DbSet<Prioridad> Prioridads { get; set; }
        public virtual DbSet<Proyecto> Proyectos { get; set; }
        public virtual DbSet<Rol> Rols { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<TipoEnlace> TipoEnlaces { get; set; }
        public virtual DbSet<TipoIdentificacion> TipoIdentificacions { get; set; }
        public virtual DbSet<TipoItemTrabajo> TipoItemTrabajos { get; set; }
        public virtual DbSet<TipoTicket> TipoTickets { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente)
                    .HasName("cliente_pkey");

                entity.ToTable("cliente");

                entity.Property(e => e.IdCliente).HasColumnName("id_cliente");

                entity.Property(e => e.Celular)
                    .IsRequired()
                    .HasMaxLength(13)
                    .HasColumnName("celular");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(35)
                    .HasColumnName("email");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("CURRENT_DATE");

                entity.Property(e => e.FechaModificacion).HasColumnName("fecha_modificacion");

                entity.Property(e => e.FkTipoIdentificacion).HasColumnName("fk_tipo_identificacion");

                entity.Property(e => e.FkUsuarioCrea).HasColumnName("fk_usuario_crea");

                entity.Property(e => e.FkUsuarioModifica).HasColumnName("fk_usuario_modifica");

                entity.Property(e => e.Identificacion)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("identificacion");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("nombre");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(9)
                    .HasColumnName("telefono");

                entity.HasOne(d => d.FkTipoIdentificacionNavigation)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.FkTipoIdentificacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cliente_fk_tipo_identificacion_fkey");

                entity.HasOne(d => d.FkUsuarioCreaNavigation)
                    .WithMany(p => p.ClienteFkUsuarioCreaNavigations)
                    .HasForeignKey(d => d.FkUsuarioCrea)
                    .HasConstraintName("cliente_fk_usuario_crea_fkey");

                entity.HasOne(d => d.FkUsuarioModificaNavigation)
                    .WithMany(p => p.ClienteFkUsuarioModificaNavigations)
                    .HasForeignKey(d => d.FkUsuarioModifica)
                    .HasConstraintName("cliente_fk_usuario_modifica_fkey");
            });

            modelBuilder.Entity<Comentario>(entity =>
            {
                entity.HasKey(e => e.IdComentario)
                    .HasName("comentario_pkey");

                entity.ToTable("comentario");

                entity.Property(e => e.IdComentario).HasColumnName("id_comentario");

                entity.Property(e => e.Contenido)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasColumnName("contenido");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("CURRENT_DATE");

                entity.Property(e => e.FechaModificacion).HasColumnName("fecha_modificacion");

                entity.Property(e => e.FkUsuarioCrea).HasColumnName("fk_usuario_crea");

                entity.Property(e => e.FkUsuarioModifica).HasColumnName("fk_usuario_modifica");

                entity.HasOne(d => d.FkUsuarioCreaNavigation)
                    .WithMany(p => p.ComentarioFkUsuarioCreaNavigations)
                    .HasForeignKey(d => d.FkUsuarioCrea)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("comentario_fk_usuario_crea_fkey");

                entity.HasOne(d => d.FkUsuarioModificaNavigation)
                    .WithMany(p => p.ComentarioFkUsuarioModificaNavigations)
                    .HasForeignKey(d => d.FkUsuarioModifica)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("comentario_fk_usuario_modifica_fkey");
            });

            modelBuilder.Entity<ComentarioItemTrabajo>(entity =>
            {
                entity.HasKey(e => e.IdComentarioItemTrabajo)
                    .HasName("comentario_item_trabajo_pkey");

                entity.ToTable("comentario_item_trabajo");

                entity.Property(e => e.IdComentarioItemTrabajo).HasColumnName("id_comentario_item_trabajo");

                entity.Property(e => e.FkComentario).HasColumnName("fk_comentario");

                entity.Property(e => e.FkItemTrabajo).HasColumnName("fk_item_trabajo");

                entity.HasOne(d => d.FkComentarioNavigation)
                    .WithMany(p => p.ComentarioItemTrabajos)
                    .HasForeignKey(d => d.FkComentario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("comentario_item_trabajo_fk_comentario_fkey");

                entity.HasOne(d => d.FkItemTrabajoNavigation)
                    .WithMany(p => p.ComentarioItemTrabajos)
                    .HasForeignKey(d => d.FkItemTrabajo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("comentario_item_trabajo_fk_item_trabajo_fkey");
            });

            modelBuilder.Entity<ComentarioProyecto>(entity =>
            {
                entity.HasKey(e => e.IdComentarioProyecto)
                    .HasName("comentario_proyecto_pkey");

                entity.ToTable("comentario_proyecto");

                entity.Property(e => e.IdComentarioProyecto).HasColumnName("id_comentario_proyecto");

                entity.Property(e => e.FkComentario).HasColumnName("fk_comentario");

                entity.Property(e => e.FkProyecto).HasColumnName("fk_proyecto");

                entity.HasOne(d => d.FkComentarioNavigation)
                    .WithMany(p => p.ComentarioProyectos)
                    .HasForeignKey(d => d.FkComentario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("comentario_proyecto_fk_comentario_fkey");

                entity.HasOne(d => d.FkProyectoNavigation)
                    .WithMany(p => p.ComentarioProyectos)
                    .HasForeignKey(d => d.FkProyecto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("comentario_proyecto_fk_proyecto_fkey");
            });

            modelBuilder.Entity<ComentarioTicket>(entity =>
            {
                entity.HasKey(e => e.IdComentarioTicket)
                    .HasName("comentario_ticket_pkey");

                entity.ToTable("comentario_ticket");

                entity.Property(e => e.IdComentarioTicket).HasColumnName("id_comentario_ticket");

                entity.Property(e => e.FkComentario).HasColumnName("fk_comentario");

                entity.Property(e => e.FkTicket).HasColumnName("fk_ticket");

                entity.HasOne(d => d.FkComentarioNavigation)
                    .WithMany(p => p.ComentarioTickets)
                    .HasForeignKey(d => d.FkComentario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("comentario_ticket_fk_comentario_fkey");

                entity.HasOne(d => d.FkTicketNavigation)
                    .WithMany(p => p.ComentarioTickets)
                    .HasForeignKey(d => d.FkTicket)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("comentario_ticket_fk_ticket_fkey");
            });

            modelBuilder.Entity<Estado>(entity =>
            {
                entity.HasKey(e => e.IdEstado)
                    .HasName("estado_pkey");

                entity.ToTable("estado");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<ItemInventario>(entity =>
            {
                entity.HasKey(e => e.IdItemInventario)
                    .HasName("item_inventario_pkey");

                entity.ToTable("item_inventario");

                entity.Property(e => e.IdItemInventario).HasColumnName("id_item_inventario");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("CURRENT_DATE");

                entity.Property(e => e.FechaModificacion).HasColumnName("fecha_modificacion");

                entity.Property(e => e.FkAsignadoA).HasColumnName("fk_asignado_a");

                entity.Property(e => e.FkUsuarioCrea).HasColumnName("fk_usuario_crea");

                entity.Property(e => e.FkUsuarioModifica).HasColumnName("fk_usuario_modifica");

                entity.Property(e => e.Licencia)
                    .HasMaxLength(500)
                    .HasColumnName("licencia");

                entity.Property(e => e.NombreSoftware)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("nombre_software");

                entity.Property(e => e.Version)
                    .HasMaxLength(10)
                    .HasColumnName("version");

                entity.HasOne(d => d.FkAsignadoANavigation)
                    .WithMany(p => p.ItemInventarioFkAsignadoANavigations)
                    .HasForeignKey(d => d.FkAsignadoA)
                    .HasConstraintName("item_inventario_fk_asignado_a_fkey");

                entity.HasOne(d => d.FkUsuarioCreaNavigation)
                    .WithMany(p => p.ItemInventarioFkUsuarioCreaNavigations)
                    .HasForeignKey(d => d.FkUsuarioCrea)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("item_inventario_fk_usuario_crea_fkey");

                entity.HasOne(d => d.FkUsuarioModificaNavigation)
                    .WithMany(p => p.ItemInventarioFkUsuarioModificaNavigations)
                    .HasForeignKey(d => d.FkUsuarioModifica)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("item_inventario_fk_usuario_modifica_fkey");
            });

            modelBuilder.Entity<ItemTrabajo>(entity =>
            {
                entity.HasKey(e => e.IdItemTrabajo)
                    .HasName("item_trabajo_pkey");

                entity.ToTable("item_trabajo");

                entity.Property(e => e.IdItemTrabajo).HasColumnName("id_item_trabajo");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasColumnName("descripcion");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("CURRENT_DATE");

                entity.Property(e => e.FechaModificacion).HasColumnName("fecha_modificacion");

                entity.Property(e => e.FkAsignadoA).HasColumnName("fk_asignado_a");

                entity.Property(e => e.FkEstado).HasColumnName("fk_estado");

                entity.Property(e => e.FkTipoItemTrabajo).HasColumnName("fk_tipo_item_trabajo");

                entity.Property(e => e.FkUsuarioCrea).HasColumnName("fk_usuario_crea");

                entity.Property(e => e.FkUsuarioModifica).HasColumnName("fk_usuario_modifica");

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasMaxLength(70)
                    .HasColumnName("titulo");

                entity.HasOne(d => d.FkAsignadoANavigation)
                    .WithMany(p => p.ItemTrabajoFkAsignadoANavigations)
                    .HasForeignKey(d => d.FkAsignadoA)
                    .HasConstraintName("item_trabajo_fk_asignado_a_fkey");

                entity.HasOne(d => d.FkEstadoNavigation)
                    .WithMany(p => p.ItemTrabajos)
                    .HasForeignKey(d => d.FkEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("item_trabajo_fk_estado_fkey");

                entity.HasOne(d => d.FkTipoItemTrabajoNavigation)
                    .WithMany(p => p.ItemTrabajos)
                    .HasForeignKey(d => d.FkTipoItemTrabajo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("item_trabajo_fk_tipo_item_trabajo_fkey");

                entity.HasOne(d => d.FkUsuarioCreaNavigation)
                    .WithMany(p => p.ItemTrabajoFkUsuarioCreaNavigations)
                    .HasForeignKey(d => d.FkUsuarioCrea)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("item_trabajo_fk_usuario_crea_fkey");

                entity.HasOne(d => d.FkUsuarioModificaNavigation)
                    .WithMany(p => p.ItemTrabajoFkUsuarioModificaNavigations)
                    .HasForeignKey(d => d.FkUsuarioModifica)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("item_trabajo_fk_usuario_modifica_fkey");
            });

            modelBuilder.Entity<ItemTrabajoEnlace>(entity =>
            {
                entity.HasKey(e => e.IdItemTrabajoEnlace)
                    .HasName("item_trabajo_enlace_pkey");

                entity.ToTable("item_trabajo_enlace");

                entity.Property(e => e.IdItemTrabajoEnlace).HasColumnName("id_item_trabajo_enlace");

                entity.Property(e => e.FkItemTrabajo1).HasColumnName("fk_item_trabajo_1");

                entity.Property(e => e.FkItemTrabajo2).HasColumnName("fk_item_trabajo_2");

                entity.Property(e => e.FkTipoEnlace).HasColumnName("fk_tipo_enlace");

                entity.HasOne(d => d.FkItemTrabajo1Navigation)
                    .WithMany(p => p.ItemTrabajoEnlaceFkItemTrabajo1Navigations)
                    .HasForeignKey(d => d.FkItemTrabajo1)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("item_trabajo_enlace_fk_item_trabajo_1_fkey");

                entity.HasOne(d => d.FkItemTrabajo2Navigation)
                    .WithMany(p => p.ItemTrabajoEnlaceFkItemTrabajo2Navigations)
                    .HasForeignKey(d => d.FkItemTrabajo2)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("item_trabajo_enlace_fk_item_trabajo_2_fkey");

                entity.HasOne(d => d.FkTipoEnlaceNavigation)
                    .WithMany(p => p.ItemTrabajoEnlaces)
                    .HasForeignKey(d => d.FkTipoEnlace)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("item_trabajo_enlace_fk_tipo_enlace_fkey");
            });

            modelBuilder.Entity<ItemTrabajoProyecto>(entity =>
            {
                entity.HasKey(e => e.IdItemTrabajoProyecto)
                    .HasName("item_trabajo_proyecto_pkey");

                entity.ToTable("item_trabajo_proyecto");

                entity.Property(e => e.IdItemTrabajoProyecto).HasColumnName("id_item_trabajo_proyecto");

                entity.Property(e => e.FkItemTrabajo).HasColumnName("fk_item_trabajo");

                entity.Property(e => e.FkProyecto).HasColumnName("fk_proyecto");

                entity.HasOne(d => d.FkItemTrabajoNavigation)
                    .WithMany(p => p.ItemTrabajoProyectos)
                    .HasForeignKey(d => d.FkItemTrabajo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("item_trabajo_proyecto_fk_item_trabajo_fkey");

                entity.HasOne(d => d.FkProyectoNavigation)
                    .WithMany(p => p.ItemTrabajoProyectos)
                    .HasForeignKey(d => d.FkProyecto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("item_trabajo_proyecto_fk_proyecto_fkey");
            });

            modelBuilder.Entity<Prioridad>(entity =>
            {
                entity.HasKey(e => e.IdPrioridad)
                    .HasName("prioridad_pkey");

                entity.ToTable("prioridad");

                entity.Property(e => e.IdPrioridad).HasColumnName("id_prioridad");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(7)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Proyecto>(entity =>
            {
                entity.HasKey(e => e.IdProyecto)
                    .HasName("proyecto_pkey");

                entity.ToTable("proyecto");

                entity.Property(e => e.IdProyecto).HasColumnName("id_proyecto");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("CURRENT_DATE");

                entity.Property(e => e.FechaModificacion).HasColumnName("fecha_modificacion");

                entity.Property(e => e.FkCliente).HasColumnName("fk_cliente");

                entity.Property(e => e.FkEstado).HasColumnName("fk_estado");

                entity.Property(e => e.FkUsuarioCrea).HasColumnName("fk_usuario_crea");

                entity.Property(e => e.FkUsuarioModifica).HasColumnName("fk_usuario_modifica");

                entity.Property(e => e.NombreProyecto)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("nombre_proyecto");

                entity.HasOne(d => d.FkClienteNavigation)
                    .WithMany(p => p.Proyectos)
                    .HasForeignKey(d => d.FkCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("proyecto_fk_cliente_fkey");

                entity.HasOne(d => d.FkEstadoNavigation)
                    .WithMany(p => p.Proyectos)
                    .HasForeignKey(d => d.FkEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("proyecto_fk_estado_fkey");

                entity.HasOne(d => d.FkUsuarioCreaNavigation)
                    .WithMany(p => p.ProyectoFkUsuarioCreaNavigations)
                    .HasForeignKey(d => d.FkUsuarioCrea)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("proyecto_fk_usuario_crea_fkey");

                entity.HasOne(d => d.FkUsuarioModificaNavigation)
                    .WithMany(p => p.ProyectoFkUsuarioModificaNavigations)
                    .HasForeignKey(d => d.FkUsuarioModifica)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("proyecto_fk_usuario_modifica_fkey");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.IdRol)
                    .HasName("rol_pkey");

                entity.ToTable("rol");

                entity.Property(e => e.IdRol).HasColumnName("id_rol");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.HasKey(e => e.IdTicket)
                    .HasName("ticket_pkey");

                entity.ToTable("ticket");

                entity.Property(e => e.IdTicket).HasColumnName("id_ticket");

                entity.Property(e => e.Contenido)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasColumnName("contenido");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("CURRENT_DATE");

                entity.Property(e => e.FechaModificacion).HasColumnName("fecha_modificacion");

                entity.Property(e => e.FkAsignadoA).HasColumnName("fk_asignado_a");

                entity.Property(e => e.FkEstado).HasColumnName("fk_estado");

                entity.Property(e => e.FkPrioridad).HasColumnName("fk_prioridad");

                entity.Property(e => e.FkTipoTicket).HasColumnName("fk_tipo_ticket");

                entity.Property(e => e.FkUsuarioCrea).HasColumnName("fk_usuario_crea");

                entity.Property(e => e.FkUsuarioModifica).HasColumnName("fk_usuario_modifica");

                entity.HasOne(d => d.FkAsignadoANavigation)
                    .WithMany(p => p.TicketFkAsignadoANavigations)
                    .HasForeignKey(d => d.FkAsignadoA)
                    .HasConstraintName("ticket_fk_asignado_a_fkey");

                entity.HasOne(d => d.FkEstadoNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.FkEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ticket_fk_estado_fkey");

                entity.HasOne(d => d.FkPrioridadNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.FkPrioridad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ticket_fk_prioridad_fkey");

                entity.HasOne(d => d.FkTipoTicketNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.FkTipoTicket)
                    .HasConstraintName("ticket_fk_tipo_ticket_fkey");

                entity.HasOne(d => d.FkUsuarioCreaNavigation)
                    .WithMany(p => p.TicketFkUsuarioCreaNavigations)
                    .HasForeignKey(d => d.FkUsuarioCrea)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ticket_fk_usuario_crea_fkey");

                entity.HasOne(d => d.FkUsuarioModificaNavigation)
                    .WithMany(p => p.TicketFkUsuarioModificaNavigations)
                    .HasForeignKey(d => d.FkUsuarioModifica)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ticket_fk_usuario_modifica_fkey");
            });

            modelBuilder.Entity<TipoEnlace>(entity =>
            {
                entity.HasKey(e => e.IdTipoEnlace)
                    .HasName("tipo_enlace_pkey");

                entity.ToTable("tipo_enlace");

                entity.Property(e => e.IdTipoEnlace).HasColumnName("id_tipo_enlace");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(15)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<TipoIdentificacion>(entity =>
            {
                entity.HasKey(e => e.IdTipoIdentificacion)
                    .HasName("tipo_identificacion_pkey");

                entity.ToTable("tipo_identificacion");

                entity.Property(e => e.IdTipoIdentificacion).HasColumnName("id_tipo_identificacion");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<TipoItemTrabajo>(entity =>
            {
                entity.HasKey(e => e.IdTipoItemTrabajo)
                    .HasName("tipo_item_trabajo_pkey");

                entity.ToTable("tipo_item_trabajo");

                entity.Property(e => e.IdTipoItemTrabajo).HasColumnName("id_tipo_item_trabajo");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<TipoTicket>(entity =>
            {
                entity.HasKey(e => e.IdTipoTicket)
                    .HasName("tipo_ticket_pkey");

                entity.ToTable("tipo_ticket");

                entity.Property(e => e.IdTipoTicket).HasColumnName("id_tipo_ticket");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("usuario_pkey");

                entity.ToTable("usuario");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("apellido");

                entity.Property(e => e.Clave)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("clave");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(35)
                    .HasColumnName("email");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasColumnName("estado")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("CURRENT_DATE");

                entity.Property(e => e.FechaModificacion).HasColumnName("fecha_modificacion");

                entity.Property(e => e.FkRol).HasColumnName("fk_rol");

                entity.Property(e => e.FkUsuarioCrea).HasColumnName("fk_usuario_crea");

                entity.Property(e => e.FkUsuarioModifica).HasColumnName("fk_usuario_modifica");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("nombre");

                entity.Property(e => e.NombreUsuario)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("nombre_usuario");

                entity.HasOne(d => d.FkRolNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.FkRol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("usuario_fk_rol_fkey");

                entity.HasOne(d => d.FkUsuarioCreaNavigation)
                    .WithMany(p => p.InverseFkUsuarioCreaNavigation)
                    .HasForeignKey(d => d.FkUsuarioCrea)
                    .HasConstraintName("usuario_fk_usuario_crea_fkey");

                entity.HasOne(d => d.FkUsuarioModificaNavigation)
                    .WithMany(p => p.InverseFkUsuarioModificaNavigation)
                    .HasForeignKey(d => d.FkUsuarioModifica)
                    .HasConstraintName("usuario_fk_usuario_modifica_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

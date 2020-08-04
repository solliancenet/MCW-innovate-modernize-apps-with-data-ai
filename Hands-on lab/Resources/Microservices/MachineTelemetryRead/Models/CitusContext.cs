using Common.Utils;
using Microsoft.EntityFrameworkCore;

namespace MachineTelemetryRead.Models
{
    public partial class CitusContext : DbContext
    {
        public CitusContext()
        {
        }

        public CitusContext(DbContextOptions<CitusContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CustomTimePartitions> CustomTimePartitions { get; set; }
        public virtual DbSet<Event1> Event1 { get; set; }
        public virtual DbSet<Event2> Event2 { get; set; }
        public virtual DbSet<Job> Job { get; set; }
        public virtual DbSet<PartConfig> PartConfig { get; set; }
        public virtual DbSet<PartConfigSub> PartConfigSub { get; set; }
        public virtual DbSet<PgBuffercache> PgBuffercache { get; set; }
        public virtual DbSet<PgDistObject> PgDistObject { get; set; }
        public virtual DbSet<PgStatStatements> PgStatStatements { get; set; }
        public virtual DbSet<TablePrivs> TablePrivs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(EnvUtils.Get("CITUS_CONNECTION_STRING"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresEnum("citus", "distribution_type", new[] { "hash", "range", "append" })
                .HasPostgresEnum("citus", "shard_transfer_mode", new[] { "auto", "force_logical", "block_writes" })
                .HasPostgresEnum("pg_catalog", "citus_copy_format", new[] { "csv", "binary", "text" })
                .HasPostgresEnum("pg_catalog", "noderole", new[] { "primary", "secondary", "unavailable" })
                .HasPostgresExtension("btree_gin")
                .HasPostgresExtension("btree_gist")
                .HasPostgresExtension("citext")
                .HasPostgresExtension("citus")
                .HasPostgresExtension("cube")
                .HasPostgresExtension("dblink")
                .HasPostgresExtension("earthdistance")
                .HasPostgresExtension("fuzzystrmatch")
                .HasPostgresExtension("hll")
                .HasPostgresExtension("hstore")
                .HasPostgresExtension("intarray")
                .HasPostgresExtension("ltree")
                .HasPostgresExtension("pg_buffercache")
                .HasPostgresExtension("pg_cron")
                .HasPostgresExtension("pg_freespacemap")
                .HasPostgresExtension("pg_partman")
                .HasPostgresExtension("pg_prewarm")
                .HasPostgresExtension("pg_stat_statements")
                .HasPostgresExtension("pg_trgm")
                .HasPostgresExtension("pgcrypto")
                .HasPostgresExtension("pgrowlocks")
                .HasPostgresExtension("pgstattuple")
                .HasPostgresExtension("session_analytics")
                .HasPostgresExtension("sslinfo")
                .HasPostgresExtension("tablefunc")
                .HasPostgresExtension("tdigest")
                .HasPostgresExtension("topn")
                .HasPostgresExtension("unaccent")
                .HasPostgresExtension("uuid-ossp")
                .HasPostgresExtension("xml2");

            modelBuilder.Entity<CustomTimePartitions>(entity =>
            {
                entity.HasKey(e => new { e.ParentTable, e.ChildTable })
                    .HasName("custom_time_partitions_pkey");

                entity.ToTable("custom_time_partitions", "partman");

                entity.HasIndex(e => e.PartitionRange)
                    .HasName("custom_time_partitions_partition_range_idx")
                    .HasMethod("gist");

                entity.Property(e => e.ParentTable).HasColumnName("parent_table");

                entity.Property(e => e.ChildTable).HasColumnName("child_table");

                entity.Property(e => e.PartitionRange)
                    .HasColumnName("partition_range")
                    .HasColumnType("tstzrange");
            });

            modelBuilder.Entity<Event1>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("event_1");

                entity.Property(e => e.EntityId)
                    .HasColumnName("entity_id")
                    .HasMaxLength(40);

                entity.Property(e => e.EntityType)
                    .HasColumnName("entity_type")
                    .HasMaxLength(70);

                entity.Property(e => e.EventData)
                    .HasColumnName("event_data")
                    .HasColumnType("json");

                entity.Property(e => e.EventId)
                    .HasColumnName("event_id")
                    .HasMaxLength(40);

                entity.Property(e => e.EventType)
                    .HasColumnName("event_type")
                    .HasMaxLength(70);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('event_id_seq'::regclass)");

                entity.Property(e => e.MachineId).HasColumnName("machine_id");
            });

            modelBuilder.Entity<Event2>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("event_2");

                entity.Property(e => e.EntityId)
                    .HasColumnName("entity_id")
                    .HasMaxLength(40);

                entity.Property(e => e.EntityType)
                    .HasColumnName("entity_type")
                    .HasMaxLength(70);

                entity.Property(e => e.EventData)
                    .HasColumnName("event_data")
                    .HasColumnType("json");

                entity.Property(e => e.EventId)
                    .HasColumnName("event_id")
                    .HasMaxLength(40);

                entity.Property(e => e.EventType)
                    .HasColumnName("event_type")
                    .HasMaxLength(70);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('event_id_seq'::regclass)");

                entity.Property(e => e.MachineId).HasColumnName("machine_id");
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.ToTable("job", "cron");

                entity.Property(e => e.Jobid)
                    .HasColumnName("jobid")
                    .HasDefaultValueSql("nextval('cron.jobid_seq'::regclass)");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasColumnName("active")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.Command)
                    .IsRequired()
                    .HasColumnName("command");

                entity.Property(e => e.Database)
                    .IsRequired()
                    .HasColumnName("database")
                    .HasDefaultValueSql("current_database()");

                entity.Property(e => e.Nodename)
                    .IsRequired()
                    .HasColumnName("nodename")
                    .HasDefaultValueSql("'localhost'::text");

                entity.Property(e => e.Nodeport)
                    .HasColumnName("nodeport")
                    .HasDefaultValueSql("inet_server_port()");

                entity.Property(e => e.Schedule)
                    .IsRequired()
                    .HasColumnName("schedule");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasDefaultValueSql("CURRENT_USER");
            });

            modelBuilder.Entity<PartConfig>(entity =>
            {
                entity.HasKey(e => e.ParentTable)
                    .HasName("part_config_parent_table_pkey");

                entity.ToTable("part_config", "partman");

                entity.HasIndex(e => e.PartitionType)
                    .HasName("part_config_type_idx");

                entity.Property(e => e.ParentTable).HasColumnName("parent_table");

                entity.Property(e => e.AutomaticMaintenance)
                    .IsRequired()
                    .HasColumnName("automatic_maintenance")
                    .HasDefaultValueSql("'on'::text");

                entity.Property(e => e.ConstraintCols).HasColumnName("constraint_cols");

                entity.Property(e => e.Control)
                    .IsRequired()
                    .HasColumnName("control");

                entity.Property(e => e.DatetimeString).HasColumnName("datetime_string");

                entity.Property(e => e.Epoch)
                    .IsRequired()
                    .HasColumnName("epoch")
                    .HasDefaultValueSql("'none'::text");

                entity.Property(e => e.InfiniteTimePartitions).HasColumnName("infinite_time_partitions");

                entity.Property(e => e.InheritFk)
                    .IsRequired()
                    .HasColumnName("inherit_fk")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.InheritPrivileges)
                    .HasColumnName("inherit_privileges")
                    .HasDefaultValueSql("false");

                entity.Property(e => e.Jobmon)
                    .IsRequired()
                    .HasColumnName("jobmon")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.OptimizeConstraint)
                    .HasColumnName("optimize_constraint")
                    .HasDefaultValueSql("30");

                entity.Property(e => e.OptimizeTrigger)
                    .HasColumnName("optimize_trigger")
                    .HasDefaultValueSql("4");

                entity.Property(e => e.PartitionInterval)
                    .IsRequired()
                    .HasColumnName("partition_interval");

                entity.Property(e => e.PartitionType)
                    .IsRequired()
                    .HasColumnName("partition_type");

                entity.Property(e => e.Premake)
                    .HasColumnName("premake")
                    .HasDefaultValueSql("4");

                entity.Property(e => e.Publications).HasColumnName("publications");

                entity.Property(e => e.Retention).HasColumnName("retention");

                entity.Property(e => e.RetentionKeepIndex)
                    .IsRequired()
                    .HasColumnName("retention_keep_index")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.RetentionKeepTable)
                    .IsRequired()
                    .HasColumnName("retention_keep_table")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.RetentionSchema).HasColumnName("retention_schema");

                entity.Property(e => e.SubPartitionSetFull).HasColumnName("sub_partition_set_full");

                entity.Property(e => e.TemplateTable).HasColumnName("template_table");

                entity.Property(e => e.TriggerExceptionHandling)
                    .HasColumnName("trigger_exception_handling")
                    .HasDefaultValueSql("false");

                entity.Property(e => e.TriggerReturnNull)
                    .IsRequired()
                    .HasColumnName("trigger_return_null")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.UndoInProgress).HasColumnName("undo_in_progress");

                entity.Property(e => e.Upsert)
                    .IsRequired()
                    .HasColumnName("upsert")
                    .HasDefaultValueSql("''::text");
            });

            modelBuilder.Entity<PartConfigSub>(entity =>
            {
                entity.HasKey(e => e.SubParent)
                    .HasName("part_config_sub_pkey");

                entity.ToTable("part_config_sub", "partman");

                entity.Property(e => e.SubParent).HasColumnName("sub_parent");

                entity.Property(e => e.SubAutomaticMaintenance)
                    .IsRequired()
                    .HasColumnName("sub_automatic_maintenance")
                    .HasDefaultValueSql("'on'::text");

                entity.Property(e => e.SubConstraintCols).HasColumnName("sub_constraint_cols");

                entity.Property(e => e.SubControl)
                    .IsRequired()
                    .HasColumnName("sub_control");

                entity.Property(e => e.SubEpoch)
                    .IsRequired()
                    .HasColumnName("sub_epoch")
                    .HasDefaultValueSql("'none'::text");

                entity.Property(e => e.SubInfiniteTimePartitions).HasColumnName("sub_infinite_time_partitions");

                entity.Property(e => e.SubInheritFk)
                    .IsRequired()
                    .HasColumnName("sub_inherit_fk")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.SubInheritPrivileges)
                    .HasColumnName("sub_inherit_privileges")
                    .HasDefaultValueSql("false");

                entity.Property(e => e.SubJobmon)
                    .IsRequired()
                    .HasColumnName("sub_jobmon")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.SubOptimizeConstraint)
                    .HasColumnName("sub_optimize_constraint")
                    .HasDefaultValueSql("30");

                entity.Property(e => e.SubOptimizeTrigger)
                    .HasColumnName("sub_optimize_trigger")
                    .HasDefaultValueSql("4");

                entity.Property(e => e.SubPartitionInterval)
                    .IsRequired()
                    .HasColumnName("sub_partition_interval");

                entity.Property(e => e.SubPartitionType)
                    .IsRequired()
                    .HasColumnName("sub_partition_type");

                entity.Property(e => e.SubPremake)
                    .HasColumnName("sub_premake")
                    .HasDefaultValueSql("4");

                entity.Property(e => e.SubRetention).HasColumnName("sub_retention");

                entity.Property(e => e.SubRetentionKeepIndex)
                    .IsRequired()
                    .HasColumnName("sub_retention_keep_index")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.SubRetentionKeepTable)
                    .IsRequired()
                    .HasColumnName("sub_retention_keep_table")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.SubRetentionSchema).HasColumnName("sub_retention_schema");

                entity.Property(e => e.SubTemplateTable).HasColumnName("sub_template_table");

                entity.Property(e => e.SubTriggerExceptionHandling)
                    .HasColumnName("sub_trigger_exception_handling")
                    .HasDefaultValueSql("false");

                entity.Property(e => e.SubTriggerReturnNull)
                    .IsRequired()
                    .HasColumnName("sub_trigger_return_null")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.SubUpsert)
                    .IsRequired()
                    .HasColumnName("sub_upsert")
                    .HasDefaultValueSql("''::text");

                entity.HasOne(d => d.SubParentNavigation)
                    .WithOne(p => p.PartConfigSub)
                    .HasForeignKey<PartConfigSub>(d => d.SubParent)
                    .HasConstraintName("part_config_sub_sub_parent_fkey");
            });

            modelBuilder.Entity<PgBuffercache>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("pg_buffercache");

                entity.Property(e => e.Bufferid).HasColumnName("bufferid");

                entity.Property(e => e.Isdirty).HasColumnName("isdirty");

                entity.Property(e => e.PinningBackends).HasColumnName("pinning_backends");

                entity.Property(e => e.Relblocknumber).HasColumnName("relblocknumber");

                entity.Property(e => e.Reldatabase)
                    .HasColumnName("reldatabase")
                    .HasColumnType("oid");

                entity.Property(e => e.Relfilenode)
                    .HasColumnName("relfilenode")
                    .HasColumnType("oid");

                entity.Property(e => e.Relforknumber).HasColumnName("relforknumber");

                entity.Property(e => e.Reltablespace)
                    .HasColumnName("reltablespace")
                    .HasColumnType("oid");

                entity.Property(e => e.Usagecount).HasColumnName("usagecount");
            });

            modelBuilder.Entity<PgDistObject>(entity =>
            {
                entity.HasKey(e => new { e.Classid, e.Objid, e.Objsubid })
                    .HasName("pg_dist_object_pkey");

                entity.ToTable("pg_dist_object", "citus");

                entity.Property(e => e.Classid)
                    .HasColumnName("classid")
                    .HasColumnType("oid");

                entity.Property(e => e.Objid)
                    .HasColumnName("objid")
                    .HasColumnType("oid");

                entity.Property(e => e.Objsubid).HasColumnName("objsubid");

                entity.Property(e => e.Colocationid).HasColumnName("colocationid");

                entity.Property(e => e.DistributionArgumentIndex).HasColumnName("distribution_argument_index");

                entity.Property(e => e.ObjectArgs).HasColumnName("object_args");

                entity.Property(e => e.ObjectNames).HasColumnName("object_names");

                entity.Property(e => e.Type).HasColumnName("type");
            });

            modelBuilder.Entity<PgStatStatements>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("pg_stat_statements");

                entity.Property(e => e.BlkReadTime).HasColumnName("blk_read_time");

                entity.Property(e => e.BlkWriteTime).HasColumnName("blk_write_time");

                entity.Property(e => e.Calls).HasColumnName("calls");

                entity.Property(e => e.Dbid)
                    .HasColumnName("dbid")
                    .HasColumnType("oid");

                entity.Property(e => e.LocalBlksDirtied).HasColumnName("local_blks_dirtied");

                entity.Property(e => e.LocalBlksHit).HasColumnName("local_blks_hit");

                entity.Property(e => e.LocalBlksRead).HasColumnName("local_blks_read");

                entity.Property(e => e.LocalBlksWritten).HasColumnName("local_blks_written");

                entity.Property(e => e.MaxTime).HasColumnName("max_time");

                entity.Property(e => e.MeanTime).HasColumnName("mean_time");

                entity.Property(e => e.MinTime).HasColumnName("min_time");

                entity.Property(e => e.Query).HasColumnName("query");

                entity.Property(e => e.Queryid).HasColumnName("queryid");

                entity.Property(e => e.Rows).HasColumnName("rows");

                entity.Property(e => e.SharedBlksDirtied).HasColumnName("shared_blks_dirtied");

                entity.Property(e => e.SharedBlksHit).HasColumnName("shared_blks_hit");

                entity.Property(e => e.SharedBlksRead).HasColumnName("shared_blks_read");

                entity.Property(e => e.SharedBlksWritten).HasColumnName("shared_blks_written");

                entity.Property(e => e.StddevTime).HasColumnName("stddev_time");

                entity.Property(e => e.TempBlksRead).HasColumnName("temp_blks_read");

                entity.Property(e => e.TempBlksWritten).HasColumnName("temp_blks_written");

                entity.Property(e => e.TotalTime).HasColumnName("total_time");

                entity.Property(e => e.Userid)
                    .HasColumnName("userid")
                    .HasColumnType("oid");
            });

            modelBuilder.Entity<TablePrivs>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("table_privs", "partman");

                entity.Property(e => e.PrivilegeType).HasColumnName("privilege_type");
            });

            modelBuilder.HasSequence("pg_dist_colocationid_seq", "pg_catalog").HasMax(4294967296);

            modelBuilder.HasSequence("pg_dist_groupid_seq", "pg_catalog").HasMax(4294967296);

            modelBuilder.HasSequence("pg_dist_node_nodeid_seq", "pg_catalog").HasMax(4294967294);

            modelBuilder.HasSequence("pg_dist_placement_placementid_seq", "pg_catalog");

            modelBuilder.HasSequence("pg_dist_shardid_seq", "pg_catalog").HasMin(102008);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

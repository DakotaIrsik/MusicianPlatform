add-migration Initial -Context AdminIdentityDbContext
add-migration Initial -Context AdminLogDbContext
add-migration Initial -Context IdentityServerConfigurationDbContext
add-migration Initial -Context IdentityServerPersistedGrantDbContext


update-database -Context AdminIdentityDbContext
update-database -Context AdminLogDbContext
update-database -Context IdentityServerConfigurationDbContext
update-database -Context IdentityServerPersistedGrantDbContext
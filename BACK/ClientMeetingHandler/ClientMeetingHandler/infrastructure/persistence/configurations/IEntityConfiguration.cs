using Microsoft.EntityFrameworkCore;

namespace ClientMeetingHandler.infrastructure.persistence.configurations;

public interface IEntityConfiguration
{
    void Configure(ModelBuilder modelBuilder);
}
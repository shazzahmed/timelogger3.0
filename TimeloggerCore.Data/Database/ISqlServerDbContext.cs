using TimeloggerCore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TimeZone = TimeloggerCore.Data.Entities.TimeZone;

namespace TimeloggerCore.Data.Database
{
    public interface ISqlServerDbContext : IDisposable
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

        int SaveChanges(CancellationToken cancellationToken = default(CancellationToken));

        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        DbSet<ApplicationUser> User { get; set; }
        DbSet<TwoFactorType> TwoFactorTypes { get; set; }
        DbSet<Addresses> Addresses { get; set; }
        DbSet<Country> Countries { get; set; }
        DbSet<City> Cities { get; set; }
        DbSet<Company> Companies { get; set; }

        DbSet<Status> Statuses { get; set; }
        DbSet<StatusType> StatusTypes { get; set; }

        DbSet<Notification> Notifications { get; set; }
        DbSet<NotificationTemplate> NotificationTemplates { get; set; }
        DbSet<NotificationType> NotificationTypes { get; set; }

        DbSet<Project> Project { get; set; }
        DbSet<TimeZone> TimeZone { get; set; }
        DbSet<Invitation> Invitations { get; set; }
        DbSet<InvitationRequest> InvitationRequest { get; set; }
        DbSet<ProjectInvitations> ProjectInvitations { get; set; }
        DbSet<TimeLog> TimeLog { get; set; }
        DbSet<Feedback> Feedback { get; set; }
        DbSet<WorkSession> WorkSession { get; set; }
        DbSet<Meeting> Meetings { get; set; }
        DbSet<CountryCode> CountryCodes { get; set; }
        DbSet<Payment> Payment { get; set; }
        DbSet<Package> Package { get; set; }
        DbSet<ClientAgency> ClientAgency { get; set; }
        DbSet<ProjectsInvitation> ProjectsInvitations { get; set; }
        DbSet<ClientWorker> ClientWorker { get; set; }
        DbSet<ProjectWorkers> ProjectWorkers { get; set; }
    }
}


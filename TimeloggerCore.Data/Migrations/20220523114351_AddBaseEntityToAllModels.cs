using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeloggerCore.Data.Migrations
{
    public partial class AddBaseEntityToAllModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "WorkSession",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "WorkSession",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "WorkSession",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "WorkSession",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "WorkSession",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "TimeLog",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "TimeLog",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TimeLog",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "TimeLog",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "TimeLog",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ProjectWorkers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ProjectWorkers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ProjectWorkers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ProjectWorkers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "ProjectWorkers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "ProjectWorkers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ProjectsInvitations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ProjectsInvitations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ProjectsInvitations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ProjectsInvitations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "ProjectsInvitations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "ProjectsInvitations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ProjectInvitations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ProjectInvitations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ProjectInvitations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ProjectInvitations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "ProjectInvitations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "ProjectInvitations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Project",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Project",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Project",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Project",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Project",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Project",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Payment",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Payment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Payment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Payment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Payment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Payment",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Payment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Package",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Package",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Package",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Package",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Package",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Package",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Package",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Notifications",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Notifications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Notifications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Notifications",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Meetings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Meetings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Meetings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Meetings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Invitations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Invitations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Invitations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Invitations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Invitations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Invitations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "InvitationRequest",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "InvitationRequest",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "InvitationRequest",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "InvitationRequest",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "InvitationRequest",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "InvitationRequest",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Feedback",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Feedback",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Feedback",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Feedback",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Feedback",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Feedback",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "CountryCodes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "CountryCodes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "CountryCodes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CountryCodes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "CountryCodes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "CountryCodes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Countries",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Countries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Countries",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Countries",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Countries",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Countries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Companies",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Companies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Companies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Companies",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ClientWorker",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ClientWorker",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ClientWorker",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ClientWorker",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "ClientWorker",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "ClientWorker",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ClientAgency",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ClientAgency",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ClientAgency",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ClientAgency",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "ClientAgency",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "ClientAgency",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Cities",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Cities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Cities",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Cities",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Cities",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Cities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Addresses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Addresses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Addresses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Addresses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "WorkSession");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "WorkSession");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "WorkSession");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "WorkSession");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "WorkSession");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "TimeLog");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "TimeLog");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TimeLog");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "TimeLog");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "TimeLog");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ProjectWorkers");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ProjectWorkers");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ProjectWorkers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ProjectWorkers");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "ProjectWorkers");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "ProjectWorkers");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ProjectsInvitations");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ProjectsInvitations");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ProjectsInvitations");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ProjectsInvitations");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "ProjectsInvitations");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "ProjectsInvitations");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ProjectInvitations");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ProjectInvitations");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ProjectInvitations");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ProjectInvitations");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "ProjectInvitations");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "ProjectInvitations");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Package");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Package");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Package");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Package");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Package");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Package");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Package");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Invitations");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Invitations");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Invitations");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Invitations");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Invitations");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Invitations");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "InvitationRequest");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "InvitationRequest");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "InvitationRequest");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "InvitationRequest");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "InvitationRequest");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "InvitationRequest");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Feedback");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Feedback");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Feedback");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Feedback");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Feedback");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Feedback");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "CountryCodes");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "CountryCodes");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "CountryCodes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CountryCodes");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "CountryCodes");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "CountryCodes");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ClientWorker");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ClientWorker");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ClientWorker");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ClientWorker");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "ClientWorker");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "ClientWorker");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ClientAgency");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ClientAgency");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ClientAgency");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ClientAgency");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "ClientAgency");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "ClientAgency");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Addresses");
        }
    }
}

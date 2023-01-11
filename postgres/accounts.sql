CREATE DATABASE "JIL_Accounts";

CREATE SCHEMA "Accounts";

CREATE TABLE "Accounts"."UserStatus"
(
    "Id" SMALLINT NOT NULL,
    "Name" TEXT NOT NULL,
    CONSTRAINT "PK_UserStatus" PRIMARY KEY("Id"),
    CONSTRAINT "UQ_UserStatus_Name" UNIQUE("Name")
);

INSERT INTO "Accounts"."UserStatus"("Id", "Name") VALUES
    (1, 'Pending'),
    (2, 'Active'),
    (3, 'Deactivated'),
    (4, 'Locked');

CREATE TABLE "Accounts"."User"
(
    "Id" INT NOT NULL GENERATED ALWAYS AS IDENTITY,
    "FirstName" TEXT NOT NULL,
    "LastName" TEXT NOT NULL,
    "Username" TEXT NOT NULL,
    "HashedPassword" TEXT NOT NULL,
    "PasswordSalt" TEXT NOT NULL,
    "StatusId" SMALLINT NOT NULL,
    "IsPasswordChangeRequired" BOOLEAN NOT NULL,
    "FullName" TEXT NOT NULL,
    "IsDeleted" BOOLEAN NOT NULL,
    "InsertedById" INT,
    "InsertedOn" TIMESTAMPTZ,
    "DeletedById" INT,
    "DeletedOn" TIMESTAMPTZ,
    CONSTRAINT "PK_User" PRIMARY KEY("Id"),
    CONSTRAINT "FK_User_StatusId" FOREIGN KEY("StatusId") REFERENCES "Accounts"."UserStatus"("Id")
);
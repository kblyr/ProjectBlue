-- CREATE DATABASE "JIL";

CREATE SCHEMA "Authorization";

CREATE TABLE "Authorization"."Role"
(
    "Id" INT NOT NULL,
    "Name" TEXT NOT NULL,
    CONSTRAINT "PK_Role" PRIMARY KEY("Id")
);

CREATE TABLE "Authorization"."UserRole"
(
    "Id" BIGINT NOT NULL GENERATED ALWAYS AS IDENTITY,
    "UserId" INT NOT NULL,
    "RoleId" INT NOT NULL,
    "IsDeleted" BOOLEAN NOT NULL,
    "InsertedById" INT,
    "InsertedOn" TIMESTAMPTZ,
    "DeletedById" INT,
    "DeletedOn" TIMESTAMPTZ,
    CONSTRAINT "PK_UserRole" PRIMARY KEY("Id"),
    CONSTRAINT "FK_UserRole_RoleId" FOREIGN KEY("RoleId") REFERENCES "Authorization"."Role"("Id")
);

CREATE TABLE "Authorization"."Permission"
(
    "Id" INT NOT NULL,
    "Name" TEXT NOT NULL,
    CONSTRAINT "PK_Permission" PRIMARY KEY("Id")
);

CREATE TABLE "Authorization"."UserPermission"
(
    "Id" BIGINT NOT NULL GENERATED ALWAYS AS IDENTITY,
    "UserId" INT NOT NULL,
    "PermissionId" INT NOT NULL,
    "IsDeleted" BOOLEAN NOT NULL,
    "InsertedById" INT,
    "InsertedOn" TIMESTAMPTZ,
    "DeletedById" INT,
    "DeletedOn" TIMESTAMPTZ,
    CONSTRAINT "PK_UserPermission" PRIMARY KEY("Id"),
    CONSTRAINT "FK_UserPermission_PermissionId" FOREIGN KEY("PermissionId") REFERENCES "Authorization"."Permission"("Id")
);
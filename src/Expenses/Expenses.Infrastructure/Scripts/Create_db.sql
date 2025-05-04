CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;
DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM pg_namespace WHERE nspname = 'expense') THEN
        CREATE SCHEMA expense;
    END IF;
END $EF$;

CREATE SEQUENCE expense.author_seq START WITH 1 INCREMENT BY 10 NO CYCLE;

CREATE SEQUENCE expense.category_seq START WITH 1 INCREMENT BY 10 NO CYCLE;

CREATE SEQUENCE expense.expense_seq START WITH 1 INCREMENT BY 10 NO CYCLE;

CREATE SEQUENCE expense.wallet_seq START WITH 1 INCREMENT BY 10 NO CYCLE;

CREATE TABLE expense.authors (
    id bigint NOT NULL,
    name character varying(255) NOT NULL,
    CONSTRAINT "PK_authors" PRIMARY KEY (id)
);

CREATE TABLE expense.wallets (
    id bigint NOT NULL,
    name character varying(255) NOT NULL,
    CONSTRAINT "PK_wallets" PRIMARY KEY (id)
);

CREATE TABLE expense.categories (
    id bigint NOT NULL,
    name character varying(255) NOT NULL,
    author_id bigint NOT NULL,
    color character varying(100),
    CONSTRAINT "PK_categories" PRIMARY KEY (id),
    CONSTRAINT "FK_categories_authors_author_id" FOREIGN KEY (author_id) REFERENCES expense.authors (id) ON DELETE CASCADE
);

CREATE TABLE expense.expenses (
    id bigint NOT NULL,
    amount numeric NOT NULL,
    currency text NOT NULL,
    transaction_date timestamp with time zone NOT NULL,
    description text,
    author_id bigint NOT NULL,
    wallet_id bigint NOT NULL,
    category_id bigint NOT NULL,
    CONSTRAINT "PK_expenses" PRIMARY KEY (id),
    CONSTRAINT "FK_expenses_authors_author_id" FOREIGN KEY (author_id) REFERENCES expense.authors (id) ON DELETE CASCADE,
    CONSTRAINT "FK_expenses_categories_category_id" FOREIGN KEY (category_id) REFERENCES expense.categories (id) ON DELETE CASCADE,
    CONSTRAINT "FK_expenses_wallets_wallet_id" FOREIGN KEY (wallet_id) REFERENCES expense.wallets (id) ON DELETE CASCADE
);

CREATE INDEX "IX_categories_author_id" ON expense.categories (author_id);

CREATE INDEX "IX_expenses_author_id" ON expense.expenses (author_id);

CREATE INDEX "IX_expenses_category_id" ON expense.expenses (category_id);

CREATE INDEX "IX_expenses_wallet_id" ON expense.expenses (wallet_id);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250302230747_InitialCreate', '9.0.2');

COMMIT;


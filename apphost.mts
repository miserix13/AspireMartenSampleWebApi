// Aspire TypeScript AppHost
// For more information, see: https://aspire.dev

import { createBuilder } from './.aspire/modules/aspire.mjs';

const builder = await createBuilder();

// Add your resources here, for example:
// const redis = await builder.addContainer("cache", "redis:latest");
// const postgres = await builder.addPostgres("db");

const postgres = await builder.addPostgres("postgres").withDataVolume();
const marten = await postgres.addDatabase("marten");

const api = await builder.addProject("api", "../AspireMartenSampleWebApi/AspireMartenSampleWebApi.csproj").withReference(marten).waitFor(marten);

await builder.build().run();
﻿using DevFreela.Core.Entities;
using DevFreela.Core.Enums;

namespace DevFreela.UnitTests.Core.Entitites;

public class ProjectTests {
    [Fact]
    public void TestIfProjectStartWorks() {
        Project project = new("Nome de Teste", "Descrição de Teste", 1, 2, 10000);

        Assert.Equal(ProjectStatusEnum.Created, project.Status);
        Assert.Null(project.StartedAt);

        Assert.NotNull(project.Title);
        Assert.NotEmpty(project.Title);

        Assert.NotNull(project.Description);
        Assert.NotEmpty(project.Description);

        project.Start();

        Assert.Equal(ProjectStatusEnum.InProgress, project.Status);
        Assert.NotNull(project.StartedAt);
    }
}
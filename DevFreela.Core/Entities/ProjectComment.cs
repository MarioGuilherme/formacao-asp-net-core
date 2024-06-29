﻿namespace DevFreela.Core.Entities;

public class ProjectComment : BaseEntity {
    public string Content { get; private set; }
    public int IdProject { get; private set; }
    public Project Project { get; private set; }
    public int IdUser { get; private set; }
    public User User { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public ProjectComment(string content, int idProject, int idUser) {
        this.Content = content;
        this.IdProject = idProject;
        this.IdUser = idUser;
    }
}
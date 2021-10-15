# Coding Standards

### C# Best Practices
- Refer to [CC# Best Practices](https://www.dofactory.com/csharp-coding-standards) for Coding Standards
- No tabs, use 2 spaces instead

### VS Code Configuration

- Install the [C# extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp) for VS Code
- Install the [Prettier extension](https://marketplace.visualstudio.com/items?itemName=esbenp.prettier-vscode) for VS Code
- Restart VS Code so Prettier detects C# formatter
- Don't make/commit changes to `.vscode/settings.json`


### Development Process

1. Pick up a task in [the current sprint](https://github.com/orgs/CIS-3760-Team-25/projects). Assign the issue to yourself and move the kanban item from To Do to In Progress

2. Create a local git branch with the format `s#/<story-name>`

   ```bash
   git checkout -b s0/create-task
   ```

3. Make changes and commit them to the branch you made

   ```bash
   git commit -m "Some message"
   ```

4. While working make sure to pull new changes from the repo so your code is up to date. Resolve merge conflicts carefully
   ```bash
   git pull origin main
   ```

5. Test the changes you made (manually or automated)

6. Push your code to GitHub

   ```bash
   git push origin <branch-name>
   ```

   Example:

   ```bash
   git push origin s0/create-task
   ```

6. Go to GitHub and create pull request

7. Put a link to your PR in the 'Pull Requests' channel on Teams

8. Wait until you get 2 approvals on your PR \
   In the meantime, check if there are any PRs that need reviews

9. Once you have 2 reviews, merge your code. Notify the team that code has been merged and that they'll need to update their local repositories

   ```bash
   git pull origin main
   ```

10. Make sure the issue associated with your story is closed and that the kanban board is updated


### Links
- Creating Pull Requests: https://docs.github.com/en/github/collaborating-with-pull-requests/proposing-changes-to-your-work-with-pull-requests/creating-a-pull-request

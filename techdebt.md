# Tech Debt

## Line endings policy for generated WinForms files

- Files: `*.Designer.cs`, potentially other Windows-oriented project files.
- Problem: the project currently has mixed or CRLF line endings in designer files, and `git diff --check` can report CRLF-added lines as trailing whitespace.
- Why it matters: routine one-line designer changes may produce noisy validation output or accidental line-ending churn.
- Recommended fix:
  - add a root `.gitattributes` with explicit `eol` rules
  - keep `*.Designer.cs` and other WinForms/project files on `crlf`
  - keep Markdown and other text docs on `lf`
  - run `git add --renormalize .` once in a separate technical commit
- Priority: low
- Scope: repository-wide metadata and line-ending normalization; do not mix with feature fixes.

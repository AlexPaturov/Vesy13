# Tech Debt

## CorrectionsForm: `_ldb` initialization

- File: `Forms/CorrectionsForm.cs`
- Problem: `_ldb` is declared as `readonly`, but the parameterless constructor does not initialize it.
- Why it matters: the WinForms designer constructor path should remain safe, and the current declaration makes the type awkward to reason about.
- Recommended fix:
  - change `_ldb` to `LocalRepository?`
  - keep the designer-friendly parameterless constructor
  - keep runtime null checks where `_ldb` is required
- Priority: medium
- Scope: small, isolated change in `CorrectionsForm`

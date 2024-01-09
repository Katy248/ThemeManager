# ThemeManager

Cli tool for managing your themes dotfiles.

## Usage

Add repository

```sh
thm repository add -u "https://some-repo.git" -n "repository-name"
```

Set new theme

```sh
thm theme set -n "ThemeName"
```

This theme will be in `~/.themes/current` folder, so now you can rewrite all your configs to link for this folder.

All application themes will be in `~/.themes/current/[application-name]` folder.

## TODO

- [ ] Specify from which repo `add`/`set` theme
- [ ] Add `config reload` command
- [ ] Add application theme `CopyOptions` implementation

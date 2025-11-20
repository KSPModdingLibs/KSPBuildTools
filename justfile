set shell := ['bash', '-c']

yaclog := "uv tool run yaclog@1.6.2"
sphinx := "uv run --project docs -m sphinx"
sphinx_autobuild := "uv run --with sphinx-autobuild --project docs sphinx-autobuild"

pin-workflows tag:
    sd '(uses: KSPModdingLibs/KSPBuildTools/\.github/actions/\S+@)\S+$' '${1}{{ tag }}' .github/workflows/*.yml

release *args:
    {{ yaclog }} release {{ args }}
    @just pin-workflows $({{ yaclog }} show --version)
    git add .github/workflows/*.yml
    {{ yaclog }} release -c

docs:
    {{ sphinx }} docs/ docs/_build

livedocs:
    {{ sphinx_autobuild }} docs/ docs/_build \
    --ignore docs/_build \
    --watch ".github"

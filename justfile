set shell:= ['bash', '-c']
yaclog:= "uv tool run yaclog@1.6.2"

pin-workflows tag:
   sed -E -H -i ".backup" "s|(uses: KSPModdingLibs/KSPBuildTools/\.github/actions/\S+@)\S+|\1{{tag}}|g" .github/workflows/*.yml

release *args:
  {{yaclog}} release {{args}}
  @just pin-workflows $({{yaclog}} show --version)
  git add .github/workflows/*.yml
  {{yaclog}} release -c

docs:
  uv run --with-requirements docs/requirements.txt --no-project -m sphinx docs/ docs/_build


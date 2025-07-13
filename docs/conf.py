# Configuration file for the Sphinx documentation builder.
#
# For the full list of built-in configuration values, see the documentation:
# https://www.sphinx-doc.org/en/master/usage/configuration.html
import os
import re
from pathlib import Path

# -- Project information -----------------------------------------------------
# https://www.sphinx-doc.org/en/master/usage/configuration.html#project-information

project = 'KSPBuildTools'
copyright = '2024, KSPModdingLibs Contributors'
author = 'KSPModdingLibs Contributors'

# -- General configuration ---------------------------------------------------
# https://www.sphinx-doc.org/en/master/usage/configuration.html#general-configuration

extensions = ['sphinx_gha', 'myst_parser', 'sphinx_copybutton', 'sphinx_jinja']

templates_path = ['_templates']
exclude_patterns = ['_build', 'Thumbs.db', '.DS_Store']

# -- Options for HTML output -------------------------------------------------
# https://www.sphinx-doc.org/en/master/usage/configuration.html#options-for-html-output

html_theme = 'sphinx_rtd_theme'
html_static_path = ['_static']

# -- Options for myst-parser -------------------------------------------------

myst_heading_anchors = 3
myst_enable_extensions = ['attrs_inline']

# -- Options for sphinx-gha --------------------------------------------------

sphinx_gha_repo_root = str(Path(__file__).parent.parent.absolute())  # docs/..
sphinx_gha_repo_slug = 'KSPModdingLibs/KSPBuildTools'

# -- Detect version ----------------------------------------------------------

nuget_version_regex = re.compile(r'(?:\.?[0-9]+){3,}(?:[-a-z]+)?')
from sphinx_gha.git_ref import get_git_ref
git_ref = get_git_ref(sphinx_gha_repo_root)

jinja_globals = {
    'git_ref': git_ref,
    'nuget_version': git_ref if nuget_version_regex.match(git_ref or "") else None,
}
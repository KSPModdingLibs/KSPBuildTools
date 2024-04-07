#!/bin/bash

shopt -s globstar

GIT_STAGE=false
DELETE_TEMPLATE_FILES=false

while getopts ":g:d:" opt; do
	case ${opt} in
		g )
			GIT_STAGE=${OPTARG}
			;;
		d )
			DELETE_TEMPLATE_FILES=${OPTARG}
			;;
		\? )
			echo "Invalid option: $OPTARG" 1>&2
			exit 1
			;;
		: )
			echo "Option -$OPTARG requires an argument." 1>&2
			exit 1
			;;
	esac
done
shift $((OPTIND -1))

VERSION_STRING="$1"; shift
TEMPLATE_EXTENSION="$1"; shift
if [ "$#" -eq 0 ]; then
	FILES=**/*$TEMPLATE_EXTENSION
else
	FILES=$@
fi

declare -A TOKENS

VERSION_FULL="$(echo $VERSION_STRING | tr -cd [\.0-9])"
TOKENS["VERSION_STRING"]=$VERSION_STRING
TOKENS["VERSION_FULL"]=$VERSION_FULL
TOKENS["VERSION_MAJOR"]="$(echo $VERSION_FULL | cut -f 1 -d '.')"
TOKENS["VERSION_MINOR"]="$(echo $VERSION_FULL | cut -f 2 -d '.')"
TOKENS["VERSION_PATCH"]="$(echo $VERSION_FULL | cut -f 3 -d '.')"
TOKENS["VERSION_BUILD"]="$(echo $VERSION_FULL | cut -f 4 -d '.')"

SED_COMMAND="-e "

for KEY in "${!TOKENS[@]}"; do
    echo "$KEY: ${TOKENS[$KEY]}"
	SED_COMMAND="${SED_COMMAND}s/@${KEY}@/${TOKENS[$KEY]}/g;"
done

for FILENAME in $FILES; do
	NEW_FILENAME="${FILENAME%$TEMPLATE_EXTENSION}"
	echo "transforming $FILENAME -> $NEW_FILENAME"
	sed "${SED_COMMAND}" "${FILENAME}" > "${NEW_FILENAME}"
	if $GIT_STAGE; then
		git add ${NEW_FILENAME}
	fi
	if [ $DELETE_TEMPLATE_FILES && $FILENAME -ne $NEW_FILENAME ] ; then
		rm ${FILENAME}
	fi
done

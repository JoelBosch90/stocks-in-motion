#!/bin/bash
################################################################################
#
#   Stocks in Motion
#
#       This bash file processes some basic actions for the Stocks in Motion
#       project. It currently supports the following commands:
#
#           update        Updates the local repository to the latest version.
#           release       Release local changes to the development branch and
#                         pushes them to the live version.
#           dev           Spins up a local development environment.
#           stable        Spins up the current stable version.
#           commit        Will commit and push the local changes on the current
#                         branch to the remote repository.
#                         (Takes an extra argument as the commit message.)
#
#   Usage example
#
#       You can use this file by executing this file and adding the commands
#       above seperated by spaces. These commands will be executed in order.
#       For example, to update the local repository and then run a local
#       development environment, you can run the following:
#
#           stocks update dev
#
#       Some command require an extra argument. You can call these like this:
#
#           stocks commit "Commit message"
#
#   Requirements
#
#       To properly use this file, there are two requirements that must be met:
#
#           Execute the following command to give this file permission to be
#           executed:
#
#               sudo chmod +x shortcuts.sh
#
#           Add a shortcut so that you can execute this file from anywhere and
#           no longer need to write the extension:
#
#               sudo ln -s shortcuts.sh /usr/bin/stocks
#
################################################################################

# Get access to the project's working directory.
WORKDIR="$(dirname "$(readlink -f "$0")")"

################################################################################
#
#   updateProject
#       Function to update the project repository.
#
################################################################################
updateProject () {

  # Visit the project directory.
  cd $WORKDIR;

  # Update to the latest version from the repository.
  git pull;
}

################################################################################
#
#   runDevelopment
#       Function to spin up a development environment.
#
################################################################################
runDevelopment () {

  # Visit the project directory.
  cd $WORKDIR;

  # Make sure we can run Docker. This config file is not needed and on Windows
  # it can cause some odd bugs causing Docker to fail.
  rm -f ~/.docker/config.json

  # Spin up the Docker Compose network with the development settings.
  docker-compose -f docker-compose.dev.yml up $1
}

################################################################################
#
#   runProduction
#       Function to spin up a production environment example.
#
################################################################################
runProduction () {

  # First update the current project so that we'll have the latest version.
  updateProject

  # Make sure we can run Docker. This config file is not needed and on Windows
  # it can cause some odd bugs causing Docker to fail.
  rm -f ~/.docker/config.json

  # Spin up the Docker Compose network with the production settings.
  docker-compose up $1
}

################################################################################
#
#   release
#       Function to release all development changes to stable. GitHub actions
#       will pick this up and automatically release them on the live server as
#       well.
#
################################################################################
release () {

  # Visit the project directory.
  cd $WORKDIR;

  # Visit the stable branch.
  git checkout stable

  # Merge the developmental changes to the stable branch.
  git merge development

  # Push the new changes to the stable branch.
  git push
}

################################################################################
#
#   commit
#       Function to commit and push all local changes to the currently selected
#       branch. Consumes one argument as the commit message.
#
################################################################################
commit () {

  # Visit the project directory.
  cd $WORKDIR;

  # Add all changes to this commit.
  git add .

  # Commit the changes with the provided message.
  git commit -m "$1"

  # Push the new changes to the currently selected branch.
  git push
}

# Loop through the command line arguments.
while [[ $# -gt 0 ]]; do

  # Give more meaningful names to the command line arguments.
  command="$1"
  argument="$2"

  # Determine per command what to do.
  case "$command" in

    # Run `stocks commit "Commit message"` to commit and push all recent
    # changes to the current branch.
    c|commit)
      commit "$argument"
      shift # Get ready to process the next command.
      shift # Skip once extra because we used an extra argument for this.
      ;;

    # Run `stocks update` to update the local repository.
    u|up|update)
      updateProject
      shift # Get ready to process the next command.
      ;;

    # Run `stocks release` to release the current version of the
    # development branch and roll those changes out to the live version.
    r|release)
      release
      shift # Get ready to process the next command.
      ;;

    # Run `stocks development` to run a local development instance of the
    # Stocks in Motion application.
    d|dev|development)
      runDevelopment "$argument"
      shift # Get ready to process the next command.
      ;;

    # Run `stocks stable` to run a local example of the stable release
    # of the Stocks in Motion application.
    s|stable)
      runProduction "$argument"
      shift # Get ready to process the next command.
      ;;
  esac
done

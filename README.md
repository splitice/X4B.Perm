X4B.Perm
========
Author: SplitIce (http://www.x4b.net)

Automatic Permission Synchronization tool.

Runs with mono on *nix to bulk set permissions in a single command, run on cron to ensure a consistent state.

Developed to correct permissions after Bittorent Sync feel free to fork.

## Config File ##
Follows the format of {GLOBBED_PATH}|{OCTAL_PERMISSIONS}.

    /root/test.sh|0700
    /var/www/*|0777
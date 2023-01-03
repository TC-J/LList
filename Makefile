relink:
	@+dotnet build
	@+dotnet publish -o llist
	@+sudo rm -rf /usr/bin/llist
	@+sudo ln -s $$PWD/llist/llist /usr/bin/llist

.PHONY: link

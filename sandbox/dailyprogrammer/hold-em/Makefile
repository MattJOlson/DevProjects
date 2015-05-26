all: card.dll

card.dll: card.cs
	mcs -t:library card.cs

card-test.dll: card-test.cs card.dll
	mcs -t:library -pkg:mono-nunit -r:card.dll card-test.cs

test: card-test.dll
	nunit-console -nologo -noresult card-test.dll

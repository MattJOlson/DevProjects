all: hold-em

hold-em: card.dll hold-em.cs
	mcs -t:exe -r:card.dll hold-em.cs

card.dll: card.cs
	mcs -t:library card.cs

card-test.dll: card.dll card-test.cs
	mcs -t:library -pkg:mono-nunit -r:card.dll card-test.cs

test: card-test.dll
	nunit-console -nologo -noresult card-test.dll

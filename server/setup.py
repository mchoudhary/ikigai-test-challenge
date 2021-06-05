import setuptools

install_requires = [
    'attrs==21.2.0', 'certifi==2021.5.30', 'chardet==4.0.0', 'click==8.0.1', 'clickclick==20.10.2', 'colorama==0.4.4', 'connexion==2.7.0', 'Flask==2.0.1', 'idna==2.10',
    'inflection==0.5.1', 'Inject==4.3.1', 'isodate==0.6.0', 'itsdangerous==2.0.1', 'Jinja2==3.0.1', 'jsonschema==3.2.0', 'MarkupSafe==2.0.1', 'munch==2.5.0', 'numpy==1.20.3',
    'openapi-schema-validator==0.1.5', 'openapi-spec-validator==0.3.1', 'pandas==1.2.4', 'pyrsistent==0.17.3', 'python-dateutil==2.8.1', 'pytz==2021.1', 'PyYAML==5.4.1',
    'requests==2.25.1', 'scipy==1.6.3', 'six==1.16.0', 'urllib3==1.26.5', 'Werkzeug==2.0.1', 'swagger-ui-bundle==0.0.6'
]

setuptools.setup(
    name="ikigai_test_challenge_api",
    version="1.0.1",
    author="mc",
    description="ikigai_test_challenge_api",
    keywords=["ikigai_test_challenge_api"],
    long_description="ikigai_test_challenge_api",
    long_description_content_type="text/markdown",
    packages=setuptools.find_packages(),
    install_requires=install_requires,
    classifiers=[
        "Intended Audience :: Developers",
        "License :: OSI Approved :: MIT License",
        "Operating System :: OS Independent",
        "Programming Language :: Python :: 3",
    ],
)

FROM microsoft/dotnet:1.0.1-sdk-projectjson
ENV name Christmas
ENV buildconfig Release
COPY src/$name /root/$name
RUN cd /root/$name && dotnet restore && dotnet build -c $buildconfig && dotnet publish -c $buildconfig
RUN cp -rf bin/$buildconfig/netcoreapp1.0/publish/* /root/
EXPOSE 80/tcp
ENTRYPOINT dotnet /root/${name}.dll

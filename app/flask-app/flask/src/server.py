import time
from concurrent import futures
from generatedImage import image
import grpc
import generateImageService_pb2
import generateImageService_pb2_grpc

class GenerateImageServiceServicer(generateImageService_pb2_grpc.GenerateImageService):

    def __init__(self):
        pass

    def generateImage(self, request, context):
        return generateImageService_pb2.ImageFile(
            Content = image,
            ContentType = "jpg"
        )

def serve():
    server = grpc.server(futures.ThreadPoolExecutor(max_workers=10))
    generateImageService_pb2_grpc.add_GenerateImageServiceServicer_to_server(GenerateImageServiceServicer(), server)
    server.add_insecure_port('[::]:50051')
    server.start()
    print('Starting gRPC sample server...')
    try:
        while True:
            time.sleep(3600)
    except KeyboardInterrupt:
        server.stop(0)

if __name__ == '__main__':
    serve()

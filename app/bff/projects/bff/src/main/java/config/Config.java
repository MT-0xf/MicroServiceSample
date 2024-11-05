package config;

import io.grpc.ManagedChannel;
import io.grpc.ManagedChannelBuilder;

/**
 * コンフィグ
 */
public class Config {
    /**
     * 主要業務チャンネル
     */
    public static ManagedChannel channel = ManagedChannelBuilder
            .forAddress("localhost", 5240)
            .usePlaintext()
            .build();
    /**
     * 画像生成チャネル
     */
    public static ManagedChannel aiChannel = ManagedChannelBuilder
            .forAddress("localhost", 50051)
            .usePlaintext()
            .build();
}
